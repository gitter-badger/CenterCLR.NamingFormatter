﻿/////////////////////////////////////////////////////////////////////////////////////////////////
//
// CenterCLR.NamingFormatter - String format library with key-valued replacer.
// Copyright (c) 2016 Kouji Matsui (@kekyo2)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//	http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CenterCLR.Tests
{
	[TestClass]
	public class FormatProviderOverloadTests
	{
		private readonly IFormatProvider formatProvider_ = new CultureInfo("fr-FR");

		[TestMethod]
		public void DictionaryOverloadTest()
		{
			var now = DateTime.Now;
			IDictionary<string, object> keyValues = new Dictionary<string, object>()
			{
				{ "abc", 123 },
				{ "defgh", now },
				{ "ijkl", "XYZ" }
			};

			var actual = Named.Format(
				formatProvider_,
				"AAA{defgh}BBB{abc}CCC{ijkl}DDD",
				keyValues);

			Assert.AreEqual("AAA" + now.ToString(formatProvider_) + "BBB123CCCXYZDDD", actual);
		}

		[TestMethod]
		public void DictionaryWithComparerOverloadTest()
		{
			var now = DateTime.Now;
			IDictionary<string, object> keyValues = new Dictionary<string, object>(
				StringComparer.InvariantCultureIgnoreCase)
			{
				{ "aBc", 123 },
				{ "dEFgh", now },
				{ "ijKl", "XYZ" }
			};

			var actual = Named.Format(
				formatProvider_,
				"AAA{Defgh}BBB{abC}CCC{IjkL}DDD",
				keyValues);

			Assert.AreEqual("AAA" + now.ToString(formatProvider_) + "BBB123CCCXYZDDD", actual);
		}

#if PCL2
		[TestMethod]
		public void ReadOnlyDictionaryOverloadTest()
		{
			var now = DateTime.Now;
			var keyValues = new Dictionary<string, object>()
			{
				{ "abc", 123 },
				{ "defgh", now },
				{ "ijkl", "XYZ" }
			};

			var actual = Named.Format<Dictionary<string, object>>(
				formatProvider_,
				"AAA{defgh}BBB{abc}CCC{ijkl}DDD",
				keyValues);

			Assert.AreEqual("AAA" + now.ToString(formatProvider_) + "BBB123CCCXYZDDD", actual);
		}
#endif

		[TestMethod]
		public void EnumerableOverloadTest()
		{
			var now = DateTime.Now;
			IEnumerable<KeyValuePair<string, object>> keyValues = new Dictionary<string, object>()
			{
				{ "abc", 123 },
				{ "defgh", now },
				{ "ijkl", "XYZ" }
			};

			var actual = Named.Format(
				formatProvider_,
				"AAA{defgh}BBB{abc}CCC{ijkl}DDD",
				keyValues);

			Assert.AreEqual("AAA" + now.ToString(formatProvider_) + "BBB123CCCXYZDDD", actual);
		}

		[TestMethod]
		public void EnumerableOverloadWithArrayTest()
		{
			var now = DateTime.Now;
			IEnumerable<KeyValuePair<string, object>> keyValues = new[]
			{
				new KeyValuePair<string, object>("abc", 123),
				new KeyValuePair<string, object>("defgh", now),
				new KeyValuePair<string, object>("ijkl", "XYZ"),
			};

			var actual = Named.Format(
				formatProvider_,
				"AAA{defgh}BBB{abc}CCC{ijkl}DDD",
				keyValues);

			Assert.AreEqual("AAA" + now.ToString(formatProvider_) + "BBB123CCCXYZDDD", actual);
		}

		[TestMethod]
		public void EnumerableOverloadWithNoListTest()
		{
			var now = DateTime.Now;
			var keyValues = new[]
			{
				Tuple.Create("abc", (object)123),
				Tuple.Create("defgh", (object)now),
				Tuple.Create("ijkl", (object)"XYZ")
			}.
			Select(entry => new KeyValuePair<string, object>(entry.Item1, entry.Item2));

			var actual = Named.Format(
				formatProvider_,
				"AAA{defgh}BBB{abc}CCC{ijkl}DDD",
				keyValues);

			Assert.AreEqual("AAA" + now.ToString(formatProvider_) + "BBB123CCCXYZDDD", actual);
		}

		[TestMethod]
		public void EnumerableOverloadWithComparerTest()
		{
			var now = DateTime.Now;
			IEnumerable<KeyValuePair<string, object>> keyValues = new[]
			{
				new KeyValuePair<string, object>("aBc", 123),
				new KeyValuePair<string, object>("deFgH", now),
				new KeyValuePair<string, object>("iJKl", "XYZ"),
			};

			var actual = Named.Format(
				formatProvider_,
				"AAA{Defgh}BBB{abC}CCC{IjkL}DDD",
				StringComparer.InvariantCultureIgnoreCase,
				keyValues);

			Assert.AreEqual("AAA" + now.ToString(formatProvider_) + "BBB123CCCXYZDDD", actual);
		}
	}
}
