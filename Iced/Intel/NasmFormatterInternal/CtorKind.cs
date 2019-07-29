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

#if !NO_NASM_FORMATTER && !NO_FORMATTER
namespace Iced.Intel.NasmFormatterInternal {
	enum CtorKind {
		Previous,
		Normal_1,
		Normal_2,
		AamAad,
		asz,
		AX,
		AY,
		bcst,
		bnd_1,
		bnd_2,
		DeclareData,
		DX,
		er_2,
		er_3,
		far,
		far_mem,
		invlpga,
		maskmovq,
		mmxmem_1,
		mmxmem_2,
		mmxmem_3,
		movabs,
		ms_pops,
		nop,
		OpSize,
		OpSize2_bnd,
		OpSize3,
		os_2,
		os_3,
		os_call_2,
		os_call_3,
		os_jcc_2,
		os_jcc_3,
		os_loop,
		os_mem,
		os_mem_reg16,
		os_mem2,
		pblendvb_1,
		pblendvb_2,
		pclmulqdq,
		pops_2,
		pops_3,
		Reg16,
		reverse2,
		sae,
		sae_pops,
		SEX1,
		SEX1a,
		SEX2_2,
		SEX2_3,
		SEX2_4,
		SEX3,
		STIG1_1,
		STIG1_2,
		STIG2_2a,
		STIG2_2b,
		xbegin,
		XLAT,
		XY,
		YA,
		YD,
		YX,
	}
}
#endif
