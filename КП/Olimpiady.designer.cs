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
	public partial class OlimpiadyDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InsertOlimpiadi(Olimpiadi instance);
    partial void UpdateOlimpiadi(Olimpiadi instance);
    partial void DeleteOlimpiadi(Olimpiadi instance);
    #endregion
		
		public OlimpiadyDataContext() : 
				base(global::КП.Properties.Settings.Default.DbConnect, mappingSource)
		{
			OnCreated();
		}
		
		public OlimpiadyDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OlimpiadyDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OlimpiadyDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OlimpiadyDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Olimpiadi> Olimpiadi
		{
			get
			{
				return this.GetTable<Olimpiadi>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Olimpiadi")]
	public partial class Olimpiadi : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID_олимпиады;
		
		private string _Название;
		
		private System.Nullable<System.DateTime> _Дата_проведения;
		
		private System.Nullable<int> _Количество_заданий;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnID_олимпиадыChanging(int value);
    partial void OnID_олимпиадыChanged();
    partial void OnНазваниеChanging(string value);
    partial void OnНазваниеChanged();
    partial void OnДата_проведенияChanging(System.Nullable<System.DateTime> value);
    partial void OnДата_проведенияChanged();
    partial void OnКоличество_заданийChanging(System.Nullable<int> value);
    partial void OnКоличество_заданийChanged();
    #endregion
		
		public Olimpiadi()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[ID олимпиады]", Storage="_ID_олимпиады", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID_олимпиады
		{
			get
			{
				return this._ID_олимпиады;
			}
			set
			{
				if ((this._ID_олимпиады != value))
				{
					this.OnID_олимпиадыChanging(value);
					this.SendPropertyChanging();
					this._ID_олимпиады = value;
					this.SendPropertyChanged("ID_олимпиады");
					this.OnID_олимпиадыChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Название", DbType="VarChar(50)")]
		public string Название
		{
			get
			{
				return this._Название;
			}
			set
			{
				if ((this._Название != value))
				{
					this.OnНазваниеChanging(value);
					this.SendPropertyChanging();
					this._Название = value;
					this.SendPropertyChanged("Название");
					this.OnНазваниеChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Дата проведения]", Storage="_Дата_проведения", DbType="Date")]
		public System.Nullable<System.DateTime> Дата_проведения
		{
			get
			{
				return this._Дата_проведения;
			}
			set
			{
				if ((this._Дата_проведения != value))
				{
					this.OnДата_проведенияChanging(value);
					this.SendPropertyChanging();
					this._Дата_проведения = value;
					this.SendPropertyChanged("Дата_проведения");
					this.OnДата_проведенияChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Количество заданий]", Storage="_Количество_заданий", DbType="Int")]
		public System.Nullable<int> Количество_заданий
		{
			get
			{
				return this._Количество_заданий;
			}
			set
			{
				if ((this._Количество_заданий != value))
				{
					this.OnКоличество_заданийChanging(value);
					this.SendPropertyChanging();
					this._Количество_заданий = value;
					this.SendPropertyChanged("Количество_заданий");
					this.OnКоличество_заданийChanged();
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
