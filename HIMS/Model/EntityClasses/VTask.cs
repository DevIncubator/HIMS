﻿//------------------------------------------------------------------------------
// <auto-generated>This code was generated by LLBLGen Pro v5.3.</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace HIMS.Data.EntityClasses
{
	/// <summary>Class which represents the entity 'VTask'.</summary>
	[Serializable]
	[DataContract(IsReference=true)]
	public partial class VTask : CommonEntityBase
	{
		#region Class Extensibility Methods
		/// <summary>Method called from the constructor</summary>
		partial void OnCreated();
		#endregion
		
		/// <summary>Initializes a new instance of the <see cref="VTask"/> class.</summary>
		public VTask() : base()
		{
			OnCreated();
		}

		#region Class Property Declarations
		/// <summary>Gets or sets the DeadlineDate field. </summary>
		[DataMember]
		public System.DateTime DeadlineDate { get; set;}
		/// <summary>Gets or sets the Description field. </summary>
		[DataMember]
		public System.String Description { get; set;}
		/// <summary>Gets or sets the Name field. </summary>
		[DataMember]
		public System.String Name { get; set;}
		/// <summary>Gets or sets the StartDate field. </summary>
		[DataMember]
		public System.DateTime StartDate { get; set;}
		/// <summary>Gets or sets the TaskId field. </summary>
		[DataMember]
		public System.Int32 TaskId { get; set;}
		#endregion
	}
}
