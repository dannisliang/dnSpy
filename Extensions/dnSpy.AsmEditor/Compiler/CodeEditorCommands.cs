﻿/*
    Copyright (C) 2014-2016 de4dot@gmail.com

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
using dnSpy.Contracts.Command;
using dnSpy.Contracts.Menus;
using dnSpy.Contracts.Text.Editor;

namespace dnSpy.AsmEditor.Compiler {
	abstract class CodeEditorCommandTargetMenuItemBase : CommandTargetMenuItemBase {
		protected CodeEditorCommandTargetMenuItemBase(StandardIds cmdId)
			: base(cmdId) {
		}

		protected override ICommandTarget GetCommandTarget(IMenuItemContext context) {
			if (context.CreatorObject.Guid != new Guid(MenuConstants.GUIDOBJ_CODE_EDITOR_GUID))
				return null;
			return context.Find<ICodeEditor>()?.TextView.CommandTarget;
		}
	}

	[ExportMenuItem(Header = "res:CutCommand", Icon = "Cut", InputGestureText = "res:ShortCutKeyCtrlX", Group = MenuConstants.GROUP_CTX_CODEEDITOR_COPY, Order = 0)]
	sealed class CutContexMenuEntry : CodeEditorCommandTargetMenuItemBase {
		CutContexMenuEntry()
			: base(StandardIds.Cut) {
		}
	}

	[ExportMenuItem(Header = "res:CopyCommand", Icon = "Copy", InputGestureText = "res:ShortCutKeyCtrlC", Group = MenuConstants.GROUP_CTX_CODEEDITOR_COPY, Order = 10)]
	sealed class CopyContexMenuEntry : CodeEditorCommandTargetMenuItemBase {
		CopyContexMenuEntry()
			: base(StandardIds.Copy) {
		}
	}

	[ExportMenuItem(Header = "res:PasteCommand", Icon = "Paste", InputGestureText = "res:ShortCutKeyCtrlV", Group = MenuConstants.GROUP_CTX_CODEEDITOR_COPY, Order = 20)]
	sealed class PasteContexMenuEntry : CodeEditorCommandTargetMenuItemBase {
		PasteContexMenuEntry()
			: base(StandardIds.Paste) {
		}
	}

	[ExportMenuItem(Header = "res:FindCommand", Icon = "Find", InputGestureText = "res:ShortCutKeyCtrlF", Group = MenuConstants.GROUP_CTX_CODEEDITOR_COPY, Order = 30)]
	sealed class FindInCodeContexMenuEntry : CodeEditorCommandTargetMenuItemBase {
		FindInCodeContexMenuEntry()
			: base(StandardIds.Find) {
		}
	}

	[ExportMenuItem(Header = "res:IncrementalSearchCommand", Icon = "Find", InputGestureText = "res:ShortCutKeyCtrlI", Group = MenuConstants.GROUP_CTX_CODEEDITOR_COPY, Order = 40)]
	sealed class IncrementalSearchForwardContexMenuEntry : CodeEditorCommandTargetMenuItemBase {
		IncrementalSearchForwardContexMenuEntry()
			: base(StandardIds.IncrementalSearchForward) {
		}
	}
}
