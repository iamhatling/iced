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

use super::super::Register;
#[cfg(not(feature = "std"))]
use alloc::string::String;
#[cfg(not(feature = "std"))]
use alloc::vec::Vec;

pub(crate) struct VARegisterValue {
	pub(crate) register: Register,
	pub(crate) element_index: usize,
	pub(crate) element_size: usize,
	pub(crate) value: u64,
}

pub(crate) struct VirtualAddressTestCase {
	pub(crate) bitness: u32,
	pub(crate) hex_bytes: String,
	pub(crate) decoder_options: u32,
	pub(crate) operand: i32,
	#[allow(dead_code)]
	pub(crate) used_mem_index: i32,
	pub(crate) element_index: usize,
	pub(crate) expected_value: u64,
	pub(crate) register_values: Vec<VARegisterValue>,
}
