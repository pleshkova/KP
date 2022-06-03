﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace КП
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="pleshkova")]
	public partial class UchastiyaDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InsertUchastie(Uchastie instance);
    partial void UpdateUchastie(Uchastie instance);
    partial void DeleteUchastie(Uchastie instance);
    #endregion
		
		public UchastiyaDataContext() : 
				base(global::КП.Properties.Settings.Default.DbConnect, mappingSource)
		{
			OnCreated();
		}
		
		public UchastiyaDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UchastiyaDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UchastiyaDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UchastiyaDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Uchastie> Uchastie
		{
			get
			{
				return this.GetTable<Uchastie>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Uchastie")]
	public partial class Uchastie : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID_участия;
		
		private System.Nullable<int> _Участник;
		
		private System.Nullable<int> _Олимпиада;
		
		private System.Nullable<int> _Баллы;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnID_участияChanging(int value);
    partial void OnID_участияChanged();
    partial void OnУчастникChanging(System.Nullable<int> value);
    partial void OnУчастникChanged();
    partial void OnОлимпиадаChanging(System.Nullable<int> value);
    partial void OnОлимпиадаChanged();
    partial void OnБаллыChanging(System.Nullable<int> value);
    partial void OnБаллыChanged();
    #endregion
		
		public Uchastie()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[ID участия]", Storage="_ID_участия", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID_участия
		{
			get
			{
				return this._ID_участия;
			}
			set
			{
				if ((this._ID_участия != value))
				{
					this.OnID_участияChanging(value);
					this.SendPropertyChanging();
					this._ID_участия = value;
					this.SendPropertyChanged("ID_участия");
					this.OnID_участияChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Участник", DbType="Int")]
		public System.Nullable<int> Участник
		{
			get
			{
				return this._Участник;
			}
			set
			{
				if ((this._Участник != value))
				{
					this.OnУчастникChanging(value);
					this.SendPropertyChanging();
					this._Участник = value;
					this.SendPropertyChanged("Участник");
					this.OnУчастникChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Олимпиада", DbType="Int")]
		public System.Nullable<int> Олимпиада
		{
			get
			{
				return this._Олимпиада;
			}
			set
			{
				if ((this._Олимпиада != value))
				{
					this.OnОлимпиадаChanging(value);
					this.SendPropertyChanging();
					this._Олимпиада = value;
					this.SendPropertyChanged("Олимпиада");
					this.OnОлимпиадаChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Баллы", DbType="Int")]
		public System.Nullable<int> Баллы
		{
			get
			{
				return this._Баллы;
			}
			set
			{
				if ((this._Баллы != value))
				{
					this.OnБаллыChanging(value);
					this.SendPropertyChanging();
					this._Баллы = value;
					this.SendPropertyChanged("Баллы");
					this.OnБаллыChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
