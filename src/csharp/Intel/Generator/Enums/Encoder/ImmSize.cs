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

namespace Generator.Enums.Encoder {
	[Enum("ImmSize")]
	enum ImmSize {
		None,
		Size1,
		Size2,
		Size4,
		Size8,
		[Comment("#(c:ENTER xxxx,yy)#")]
		Size2_1,
		[Comment("#(c:EXTRQ/INSERTQ xx,yy)#")]
		Size1_1,
		[Comment("#(c:CALL16 FAR x:y)#")]
		Size2_2,
		[Comment("#(c:CALL32 FAR x:y)#")]
		Size4_2,
		RipRelSize1_Target16,
		RipRelSize1_Target32,
		RipRelSize1_Target64,
		RipRelSize2_Target16,
		RipRelSize2_Target32,
		RipRelSize2_Target64,
		RipRelSize4_Target32,
		RipRelSize4_Target64,
		SizeIbReg,
		Size1OpCode,
	}
}
