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
	public partial class UchastnikiDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InsertUchastniky(Uchastniky instance);
    partial void UpdateUchastniky(Uchastniky instance);
    partial void DeleteUchastniky(Uchastniky instance);
    #endregion
		
		public UchastnikiDataContext() : 
				base(global::КП.Properties.Settings.Default.DbConnect, mappingSource)
		{
			OnCreated();
		}
		
		public UchastnikiDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UchastnikiDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UchastnikiDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UchastnikiDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Uchastniky> Uchastniky
		{
			get
			{
				return this.GetTable<Uchastniky>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Uchastniky")]
	public partial class Uchastniky : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID_участника;
		
		private string _ФИО;
		
		private System.Nullable<System.DateTime> _Дата_рождения;
		
		private string _Телефон;
		
		private string _Адрес;
		
		private string _Руководитель;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnID_участникаChanging(int value);
    partial void OnID_участникаChanged();
    partial void OnФИОChanging(string value);
    partial void OnФИОChanged();
    partial void OnДата_рожденияChanging(System.Nullable<System.DateTime> value);
    partial void OnДата_рожденияChanged();
    partial void OnТелефонChanging(string value);
    partial void OnТелефонChanged();
    partial void OnАдресChanging(string value);
    partial void OnАдресChanged();
    partial void OnРуководительChanging(string value);
    partial void OnРуководительChanged();
    #endregion
		
		public Uchastniky()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[ID участника]", Storage="_ID_участника", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID_участника
		{
			get
			{
				return this._ID_участника;
			}
			set
			{
				if ((this._ID_участника != value))
				{
					this.OnID_участникаChanging(value);
					this.SendPropertyChanging();
					this._ID_участника = value;
					this.SendPropertyChanged("ID_участника");
					this.OnID_участникаChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ФИО", DbType="VarChar(70)")]
		public string ФИО
		{
			get
			{
				return this._ФИО;
			}
			set
			{
				if ((this._ФИО != value))
				{
					this.OnФИОChanging(value);
					this.SendPropertyChanging();
					this._ФИО = value;
					this.SendPropertyChanged("ФИО");
					this.OnФИОChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Дата рождения]", Storage="_Дата_рождения", DbType="Date", IsDbGenerated=true)]
		public System.Nullable<System.DateTime> Дата_рождения
		{
			get
			{
				return this._Дата_рождения;
			}
			set
			{
				if ((this._Дата_рождения != value))
				{
					this.OnДата_рожденияChanging(value);
					this.SendPropertyChanging();
					this._Дата_рождения = value;
					this.SendPropertyChanged("Дата_рождения");
					this.OnДата_рожденияChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Телефон", DbType="VarChar(50)")]
		public string Телефон
		{
			get
			{
				return this._Телефон;
			}
			set
			{
				if ((this._Телефон != value))
				{
					this.OnТелефонChanging(value);
					this.SendPropertyChanging();
					this._Телефон = value;
					this.SendPropertyChanged("Телефон");
					this.OnТелефонChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Адрес", DbType="VarChar(50)")]
		public string Адрес
		{
			get
			{
				return this._Адрес;
			}
			set
			{
				if ((this._Адрес != value))
				{
					this.OnАдресChanging(value);
					this.SendPropertyChanging();
					this._Адрес = value;
					this.SendPropertyChanged("Адрес");
					this.OnАдресChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Руководитель", DbType="VarChar(50)")]
		public string Руководитель
		{
			get
			{
				return this._Руководитель;
			}
			set
			{
				if ((this._Руководитель != value))
				{
					this.OnРуководительChanging(value);
					this.SendPropertyChanging();
					this._Руководитель = value;
					this.SendPropertyChanged("Руководитель");
					this.OnРуководительChanged();
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
