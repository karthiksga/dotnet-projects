﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LinqTest
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Silverlight")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::LinqTest.Properties.Settings.Default.SilverlightConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Table2> Table2s
		{
			get
			{
				return this.GetTable<Table2>();
			}
		}
		
		public System.Data.Linq.Table<Table3> Table3s
		{
			get
			{
				return this.GetTable<Table3>();
			}
		}
		
		public System.Data.Linq.Table<Table1> Table1s
		{
			get
			{
				return this.GetTable<Table1>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Table2")]
	public partial class Table2
	{
		
		private System.Nullable<int> _tID;
		
		private System.Nullable<int> _ColID;
		
		private string _Txt;
		
		public Table2()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tID", DbType="Int")]
		public System.Nullable<int> tID
		{
			get
			{
				return this._tID;
			}
			set
			{
				if ((this._tID != value))
				{
					this._tID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ColID", DbType="Int")]
		public System.Nullable<int> ColID
		{
			get
			{
				return this._ColID;
			}
			set
			{
				if ((this._ColID != value))
				{
					this._ColID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Txt", DbType="VarChar(10)")]
		public string Txt
		{
			get
			{
				return this._Txt;
			}
			set
			{
				if ((this._Txt != value))
				{
					this._Txt = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Table3")]
	public partial class Table3
	{
		
		private System.Nullable<int> _tID;
		
		private string _Field1;
		
		private string _Field2;
		
		private string _Field3;
		
		public Table3()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tID", DbType="Int")]
		public System.Nullable<int> tID
		{
			get
			{
				return this._tID;
			}
			set
			{
				if ((this._tID != value))
				{
					this._tID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Field1", DbType="VarChar(50)")]
		public string Field1
		{
			get
			{
				return this._Field1;
			}
			set
			{
				if ((this._Field1 != value))
				{
					this._Field1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Field2", DbType="VarChar(50)")]
		public string Field2
		{
			get
			{
				return this._Field2;
			}
			set
			{
				if ((this._Field2 != value))
				{
					this._Field2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Field3", DbType="VarChar(50)")]
		public string Field3
		{
			get
			{
				return this._Field3;
			}
			set
			{
				if ((this._Field3 != value))
				{
					this._Field3 = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Table1")]
	public partial class Table1
	{
		
		private System.Nullable<int> _ColId;
		
		private string _ColName;
		
		private string _FieldName;
		
		public Table1()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ColId", DbType="Int")]
		public System.Nullable<int> ColId
		{
			get
			{
				return this._ColId;
			}
			set
			{
				if ((this._ColId != value))
				{
					this._ColId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ColName", DbType="VarChar(10)")]
		public string ColName
		{
			get
			{
				return this._ColName;
			}
			set
			{
				if ((this._ColName != value))
				{
					this._ColName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FieldName", DbType="VarChar(50)")]
		public string FieldName
		{
			get
			{
				return this._FieldName;
			}
			set
			{
				if ((this._FieldName != value))
				{
					this._FieldName = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
