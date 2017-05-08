﻿/*
    Copyright (C) 2014-2017 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using dnSpy.Contracts.Text;

namespace dnSpy.Contracts.Debugger.Evaluation {
	/// <summary>
	/// A value shown in a treeview (eg. in locals window)
	/// </summary>
	public abstract class DbgValueNode : DbgObject {
		/// <summary>
		/// Gets the language
		/// </summary>
		public abstract DbgLanguage Language { get; }

		/// <summary>
		/// Gets the process
		/// </summary>
		public DbgProcess Process => Thread.Process;

		/// <summary>
		/// Gets the runtime
		/// </summary>
		public DbgRuntime Runtime => Thread.Runtime;

		/// <summary>
		/// Gets the thread
		/// </summary>
		public abstract DbgThread Thread { get; }

		/// <summary>
		/// Gets the value
		/// </summary>
		public abstract DbgValue Value { get; }

		/// <summary>
		/// Gets the expression that is used when adding an expression to the watch window or
		/// when assigning a new value to the source.
		/// </summary>
		public abstract string Expression { get; }

		/// <summary>
		/// Image name, see <see cref="PredefinedDbgValueNodeImageNames"/>
		/// </summary>
		public abstract string ImageName { get; }

		/// <summary>
		/// true if this is a read-only value
		/// </summary>
		public abstract bool IsReadOnly { get; }

		/// <summary>
		/// Returns true if it has children, false if it has no children and null if it's unknown (eg. it's too expensive to calculate it now).
		/// UI code can use this property to decide if it shows the treeview node expander ("|>").
		/// </summary>
		public abstract bool? HasChildren { get; }

		/// <summary>
		/// Number of children. This property is called as late as possible and can be lazily initialized.
		/// It's assumed to be 0 if <see cref="HasChildren"/> is false.
		/// </summary>
		public abstract ulong ChildrenCount { get; }

		/// <summary>
		/// Creates new children. This method blocks the current thread until the children have been created.
		/// </summary>
		/// <param name="index">Index of first child</param>
		/// <param name="count">Max number of children to return</param>
		/// <returns></returns>
		public abstract DbgValueNode[] GetChildren(ulong index, int count);

		/// <summary>
		/// Creates new children
		/// </summary>
		/// <param name="index">Index of first child</param>
		/// <param name="count">Max number of children to return</param>
		/// <param name="callback">Called when this method is complete</param>
		public abstract void GetChildren(ulong index, int count, Action<DbgValueNode[]> callback);

		/// <summary>
		/// Formats the name. This method blocks the current thread until all requested values have been formatted
		/// </summary>
		/// <param name="output">Output</param>
		public void FormatName(ITextColorWriter output) =>
			Format(new DbgValueNodeFormatParameters { NameWriter = output ?? throw new ArgumentNullException(nameof(output)) });

		/// <summary>
		/// Formats the value. This method blocks the current thread until all requested values have been formatted
		/// </summary>
		/// <param name="output">Output</param>
		/// <param name="options">Formatter options</param>
		public void FormatValue(ITextColorWriter output, DbgValueFormatterOptions options) =>
			Format(new DbgValueNodeFormatParameters {
				ValueWriter = output ?? throw new ArgumentNullException(nameof(output)),
				ValueFormatterOptions = options,
			});

		/// <summary>
		/// Formats the expected type ("field" type). This method blocks the current thread until all requested values have been formatted
		/// </summary>
		/// <param name="output">Output</param>
		/// <param name="options">Formatter options</param>
		public void FormatExpectedType(ITextColorWriter output, DbgValueFormatterTypeOptions options) =>
			Format(new DbgValueNodeFormatParameters {
				ExpectedTypeWriter = output ?? throw new ArgumentNullException(nameof(output)),
				ExpectedTypeFormatterOptions = options,
			});

		/// <summary>
		/// Formats the actual type (value type). This method blocks the current thread until all requested values have been formatted
		/// </summary>
		/// <param name="output">Output</param>
		/// <param name="options">Formatter options</param>
		public void FormatActualType(ITextColorWriter output, DbgValueFormatterTypeOptions options) =>
			Format(new DbgValueNodeFormatParameters {
				ActualTypeWriter = output ?? throw new ArgumentNullException(nameof(output)),
				ActualTypeFormatterOptions = options,
			});

		/// <summary>
		/// Formats the name, value, and type. This method blocks the current thread until all requested values have been formatted
		/// </summary>
		/// <param name="options">Options</param>
		public abstract void Format(IDbgValueNodeFormatParameters options);

		/// <summary>
		/// Formats the name, value, and type
		/// </summary>
		/// <param name="options">Options</param>
		/// <param name="callback">Called when this method is complete</param>
		public abstract void Format(IDbgValueNodeFormatParameters options, Action callback);
	}

	/// <summary>
	/// Parameters when formatting a <see cref="DbgValueNode"/>
	/// </summary>
	public interface IDbgValueNodeFormatParameters {
		/// <summary>
		/// Used when writing the name or null if it's not written
		/// </summary>
		ITextColorWriter NameWriter { get; }

		/// <summary>
		/// Used when writing the value or null if it's not written
		/// </summary>
		ITextColorWriter ValueWriter { get; }

		/// <summary>
		/// Used when writing the expected type ("field" type) or null if it's not written
		/// </summary>
		ITextColorWriter ExpectedTypeWriter { get; }

		/// <summary>
		/// Used when writing the actual type (value type) or null if it's not written
		/// </summary>
		ITextColorWriter ActualTypeWriter { get; }

		/// <summary>
		/// Value formatter options
		/// </summary>
		DbgValueFormatterOptions ValueFormatterOptions { get; }

		/// <summary>
		/// Expected type formatter options
		/// </summary>
		DbgValueFormatterTypeOptions ExpectedTypeFormatterOptions { get; }

		/// <summary>
		/// Actual type formatter options
		/// </summary>
		DbgValueFormatterTypeOptions ActualTypeFormatterOptions { get; }
	}

	sealed class DbgValueNodeFormatParameters : IDbgValueNodeFormatParameters {
		public ITextColorWriter NameWriter { get; set; }
		public ITextColorWriter ValueWriter { get; set; }
		public ITextColorWriter ExpectedTypeWriter { get; set; }
		public ITextColorWriter ActualTypeWriter { get; set; }
		public DbgValueFormatterOptions ValueFormatterOptions { get; set; }
		public DbgValueFormatterTypeOptions ExpectedTypeFormatterOptions { get; set; }
		public DbgValueFormatterTypeOptions ActualTypeFormatterOptions { get; set; }
	}
}
