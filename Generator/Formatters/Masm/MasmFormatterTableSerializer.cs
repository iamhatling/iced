/*
Copyright (C) 2018-2019 de4dot@gmail.com

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

#if !NO_MASM_FORMATTER && !NO_FORMATTER
using System;
using System.IO;
using System.Text;
using Generator.IO;
using Iced.Intel;
using Iced.Intel.MasmFormatterInternal;

namespace Generator.Formatters.Masm {
	sealed class MasmFormatterTableSerializer : FormatterTableSerializer {
		public override void Initialize(StringsTable stringsTable) {
			foreach (var info in CtorInfos.Infos) {
				foreach (var o in info) {
					if (o is string s)
						stringsTable.Add(s);
				}
			}
		}

		public override string GetFilename(string icedProjectDir) =>
			Path.Combine(icedProjectDir, "Intel", "MasmFormatterInternal", "InstrInfos.g.cs");

		public override void Serialize(FileWriter writer, StringsTable stringsTable) {
			writer.WriteHeader();
			writer.WriteLine("#if !NO_MASM_FORMATTER && !NO_FORMATTER");
			writer.WriteLine("namespace Iced.Intel.MasmFormatterInternal {");
			writer.Indent();
			writer.WriteLine("static partial class InstrInfos {");
			writer.Indent();
			writer.WriteLine("static byte[] GetSerializedInstrInfos() =>");
			writer.Indent();
			writer.WriteLine("new byte[] {");
			writer.Indent();

			int index = -1;
			var sb = new StringBuilder();
			var infos = CtorInfos.Infos;
			for (int i = 0; i < infos.Length; i++) {
				var info = infos[i];
				index++;
				var ctorKind = (CtorKind)info[0];
				var code = (Code)info[1];
				if (code != (Code)index)
					throw new InvalidOperationException();

				if (index != 0)
					writer.WriteLine();
				writer.WriteComment(code.ToString());
				writer.WriteLine();

				if (i > 0 && IsSame(infos[i - 1], info)) {
					writer.WriteByte((byte)CtorKind.Previous);
					writer.WriteComment(nameof(CtorKind.Previous));
					writer.WriteLine();
					continue;
				}

				if ((uint)ctorKind > byte.MaxValue)
					throw new InvalidOperationException();
				writer.WriteByte((byte)ctorKind);
				writer.WriteComment($"{ctorKind}");
				writer.WriteLine();
				uint si;
				for (int j = 2; j < info.Length; j++) {
					switch (info[j]) {
					case string s:
						si = stringsTable.GetIndex(s);
						writer.WriteCompressedUInt32(si);
						writer.WriteComment($"{si} = \"{s}\"");
						break;

					case char c:
						if ((ushort)c > byte.MaxValue)
							throw new InvalidOperationException();
						writer.WriteByte((byte)c);
						if (c == '\0')
							writer.WriteComment(@"'\0'");
						else
							writer.WriteComment($"'{c}'");
						break;

					case InstrOpInfoFlags flags:
						writer.WriteCompressedUInt32((uint)flags);
						writer.WriteComment($"0x{(uint)flags:X} = {ToString(sb, flags)}");
						break;

					case int ival:
						writer.WriteCompressedUInt32((uint)ival);
						writer.WriteComment($"0x{ival:X}");
						break;

					case PseudoOpsKind pseudoOpsKind:
						if ((uint)pseudoOpsKind > byte.MaxValue)
							throw new InvalidOperationException();
						writer.WriteByte((byte)pseudoOpsKind);
						writer.WriteComment(pseudoOpsKind.ToString());
						break;

					case CodeSize codeSize:
						if ((uint)codeSize > byte.MaxValue)
							throw new InvalidOperationException();
						writer.WriteByte((byte)codeSize);
						writer.WriteComment(codeSize.ToString());
						break;

					case Register register:
						if ((uint)register > byte.MaxValue)
							throw new InvalidOperationException();
						writer.WriteByte((byte)register);
						writer.WriteComment(register.ToString());
						break;

					case bool b:
						writer.WriteByte((byte)(b ? 1 : 0));
						writer.WriteComment(b.ToString());
						break;

					default:
						throw new InvalidOperationException();
					}
					writer.WriteLine();
				}
			}

			writer.Unindent();
			writer.WriteLine("};");
			writer.Unindent();
			writer.Unindent();
			writer.WriteLine("}");
			writer.Unindent();
			writer.WriteLine("}");
			writer.WriteLine("#endif");
		}

		static string ToString(StringBuilder sb, InstrOpInfoFlags flags) {
			sb.Clear();

			switch (flags & (InstrOpInfoFlags)((int)InstrOpInfoFlags.MemSize_Mask << 0)) {
			case 0: break;
			case InstrOpInfoFlags.MemSize_Mmx: Append(sb, nameof(InstrOpInfoFlags.MemSize_Mmx)); break;
			case InstrOpInfoFlags.MemSize_Normal: Append(sb, nameof(InstrOpInfoFlags.MemSize_Normal)); break;
			case InstrOpInfoFlags.MemSize_Nothing: Append(sb, nameof(InstrOpInfoFlags.MemSize_Nothing)); break;
			case InstrOpInfoFlags.MemSize_XmmwordPtr: Append(sb, nameof(InstrOpInfoFlags.MemSize_XmmwordPtr)); break;
			case InstrOpInfoFlags.MemSize_DwordOrQword: Append(sb, nameof(InstrOpInfoFlags.MemSize_DwordOrQword)); break;
			default: throw new InvalidOperationException();
			}
			flags &= ~(InstrOpInfoFlags)((int)InstrOpInfoFlags.MemSize_Mask << 0);

			if ((flags & InstrOpInfoFlags.ShowNoMemSize_ForceSize) != 0) {
				flags &= ~InstrOpInfoFlags.ShowNoMemSize_ForceSize;
				Append(sb, nameof(InstrOpInfoFlags.ShowNoMemSize_ForceSize));
			}

			if ((flags & InstrOpInfoFlags.ShowMinMemSize_ForceSize) != 0) {
				flags &= ~InstrOpInfoFlags.ShowMinMemSize_ForceSize;
				Append(sb, nameof(InstrOpInfoFlags.ShowMinMemSize_ForceSize));
			}

			if ((flags & InstrOpInfoFlags.JccNotTaken) != 0) {
				flags &= ~InstrOpInfoFlags.JccNotTaken;
				Append(sb, nameof(InstrOpInfoFlags.JccNotTaken));
			}

			if ((flags & InstrOpInfoFlags.JccTaken) != 0) {
				flags &= ~InstrOpInfoFlags.JccTaken;
				Append(sb, nameof(InstrOpInfoFlags.JccTaken));
			}

			if ((flags & InstrOpInfoFlags.BndPrefix) != 0) {
				flags &= ~InstrOpInfoFlags.BndPrefix;
				Append(sb, nameof(InstrOpInfoFlags.BndPrefix));
			}

			if ((flags & InstrOpInfoFlags.IgnoreIndexReg) != 0) {
				flags &= ~InstrOpInfoFlags.IgnoreIndexReg;
				Append(sb, nameof(InstrOpInfoFlags.IgnoreIndexReg));
			}

			if ((flags & InstrOpInfoFlags.MnemonicIsDirective) != 0) {
				flags &= ~InstrOpInfoFlags.MnemonicIsDirective;
				Append(sb, nameof(InstrOpInfoFlags.MnemonicIsDirective));
			}

			if (flags != 0)
				throw new InvalidOperationException();

			if (sb.Length == 0)
				Append(sb, nameof(InstrOpInfoFlags.None));

			return sb.ToString();
		}

		static void Append(StringBuilder sb, string name) {
			if (sb.Length > 0)
				sb.Append(", ");
			sb.Append(name);
		}
	}
}
#endif
