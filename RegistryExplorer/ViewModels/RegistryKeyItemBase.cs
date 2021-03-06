﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Diagnostics;
using System.Windows;

namespace RegistryExplorer.ViewModels {
	[Flags]
	enum RegistryKeyFlags {
		Favorite = 1,
	}

	abstract class RegistryKeyItemBase : BindableBase {
		private string _text;
		protected ObservableCollection<RegistryKeyItemBase> _subItems;

		public string Path { get; protected set; }

		public RegistryKeyFlags Flags { get; set; }

		public RegistryKeyItemBase Parent { get; private set; }

		public virtual ObservableCollection<RegistryKeyItemBase> SubItems {
			get {
				if(_subItems == null)
					_subItems = new ObservableCollection<RegistryKeyItemBase>();
				return _subItems; 
			}
		}

		protected RegistryKeyItemBase(RegistryKeyItemBase parent) {
			Parent = parent;
		}

		public string TypeName => GetType().Name;

		public virtual void Refresh() { }

		public string Text {
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}

		private bool _isExpanded;

		public bool IsExpanded {
			get { return _isExpanded; }
			set { SetProperty(ref _isExpanded, value); }
		}

		private bool _isSelected;

		public bool IsSelected {
			get { return _isSelected; }
			set { SetProperty(ref _isSelected, value); }
		}

		public T GetSubItem<T>(string name) where T : RegistryKeyItemBase {
			return SubItems.FirstOrDefault(i => i.Text.Equals(name, StringComparison.InvariantCultureIgnoreCase)) as T;
		}

		public override string ToString() {
			return _text.ToString();
		}
	}
}
