// Copyright 2005, 2006 - Morten Nielsen (www.iter.dk)
//
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Utilities;

namespace SharpMap.CoordinateSystems
{
	/// <summary>
	/// A named parameter value.
	/// </summary>
	public class Parameter
	{
		/// <summary>
		/// Creates an instance of a parameter
		/// </summary>
		/// <remarks>Units are always either meters or degrees.</remarks>
		/// <param name="name">Name of parameter</param>
		/// <param name="value">Value</param>
		public Parameter(string name, double value)
		{
			_Name = name;
			_Value = Value;
		}

		public override string ToString()
		{
			return String.Format("[Parameter] {0} = {1}", Name, Value);
		}

		public override bool Equals(object obj)
		{
			Parameter other = obj as Parameter;

			if (other == null)
			{
				return false;
			}

			return other.Name == Name && Tolerance.Equal<Parameter>(Value, other.Value);
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode() ^ Value.GetHashCode();
		}

		#region IParameter Members

		private string _Name;

		/// <summary>
		/// Parameter name
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		private double _Value;

		/// <summary>
		/// Parameter value
		/// </summary>
		public double Value
		{
			get { return _Value; }
			set { _Value = value; }
		}
	
		#endregion
	}
}
