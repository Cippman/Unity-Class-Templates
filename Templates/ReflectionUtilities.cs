using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Cipp.ClassCreation {

	[System.Serializable]
	public struct PropertyInfoRef {
		public string propertyInfoName;
		public string propertyInfoType;
		public string propertyDeclaringType;
		public string propertyInfoValue;

		public PropertyInfoRef (object obj, PropertyInfo propertyInfo) {
			this.propertyInfoName = propertyInfo.Name;
			this.propertyInfoType = propertyInfo.PropertyType.Name;
			this.propertyDeclaringType = propertyInfo.DeclaringType.Name;
			this.propertyInfoValue = string.Format("{0}", propertyInfo.GetValue (obj, null));
		}

		public static void PrintThis (PropertyInfoRef value) {
			Debug.Log (value.propertyInfoType+" "+value.propertyInfoName+ " = "+value.propertyInfoValue);
			Debug.Log ("Type: "+value.propertyDeclaringType);
		}
	}

	[System.Serializable]
	public struct MethodInfoRef {
		public string methodInfoName;
		public string methodInfoArguments;
		public string methodInfoVoid;
		public ReflectionUtilities.MethodProperties methodInfoProperties;

		public MethodInfoRef (MethodInfo methodInfo) {
			this.methodInfoName = methodInfo.Name;
			this.methodInfoArguments = ReflectionUtilities.Parameters (methodInfo.GetParameters());
			this.methodInfoVoid = methodInfo.Name + " ("+this.methodInfoArguments+")";
			this.methodInfoProperties = new ReflectionUtilities.MethodProperties (methodInfo.IsPublic, methodInfo.IsStatic, methodInfo.IsVirtual, false);
		}
	}

	[System.Serializable]
	public struct FieldInfoRef {
		public string fieldInfoName;
		public string fieldInfoType;
		public string fieldInfoDeclaringType;
		public string fieldInfoValue;

		public FieldInfoRef (object obj,FieldInfo fieldInfo) {
			this.fieldInfoName = fieldInfo.Name;
			this.fieldInfoType = fieldInfo.FieldType.Name;
			this.fieldInfoDeclaringType = fieldInfo.DeclaringType.Name;
			this.fieldInfoValue = string.Format("{0}", fieldInfo.GetValue (obj));
		}
		public static void PrintThis (FieldInfoRef value) {
			Debug.Log (value.fieldInfoType+" "+value.fieldInfoName+ " = "+value.fieldInfoValue);
			Debug.Log ("Type: "+value.fieldInfoDeclaringType);
		}
	}

	public static class ReflectionUtilities {

		public struct MethodProperties{
			public bool isPublic;
			public bool isStatic;
			public bool isVirtual;
			public bool isOverride;

			public MethodProperties (bool isPublic, bool isStatic, bool isVirtual, bool isOverride) {
				this.isPublic = isPublic;
				this.isStatic = isStatic;
				this.isVirtual = isVirtual;
				this.isOverride = isOverride;
			}
		}

		public static BindingFlags[] DefaultFlags () {
			BindingFlags [] tmp = new BindingFlags[3];
			tmp [0] = BindingFlags.Public;
			tmp [1] = BindingFlags.Instance;
			tmp [2] = BindingFlags.DeclaredOnly;
			return tmp;
		}

		public static List<PropertyInfoRef> ClassDefaultProperties (object obj) {
			List<PropertyInfoRef> tmpPropertyRefs = new List<PropertyInfoRef> ();
			Type type = obj.GetType ();
			BindingFlags[] flags = ReflectionUtilities.DefaultFlags ();
			PropertyInfo[] properties = type.GetProperties (flags [0] | flags [1] | flags [2] | BindingFlags.GetProperty | BindingFlags.SetProperty);
			for (int i = 0; i < properties.Length; i++) {
				PropertyInfoRef tmpPropRef = new PropertyInfoRef (obj, properties [i]);
				if (!tmpPropertyRefs.Contains (tmpPropRef)) {
					tmpPropertyRefs.Add (tmpPropRef);
				}
			}
			return tmpPropertyRefs;
		}

		public static List<MethodInfoRef> ClassDefaultMethods (object obj){
			List<MethodInfoRef>	tmpMethodsRefs = new List<MethodInfoRef> ();
			Type type = obj.GetType ();
			BindingFlags[] flags = ReflectionUtilities.DefaultFlags ();
			MethodInfo[] methods = type.GetMethods (flags[0] | flags[1] | flags[2] | BindingFlags.InvokeMethod);
			for (int i = 0; i < methods.Length; i++) {
				MethodInfoRef tmpMethodRef = new MethodInfoRef (methods[i]);
				if (!tmpMethodsRefs.Contains (tmpMethodRef)) {
					tmpMethodsRefs.Add (tmpMethodRef);
				}
			}
			return tmpMethodsRefs;
		}
		public static string Parameters (ParameterInfo[] parameters) {
			string effectiveValue = "";
			for (int i = 0; i < parameters.Length; i++){
				string tmp = "";
				if (i == parameters.Length - 1) {
					tmp = ""+parameters [i].ParameterType.Name+" "+parameters [i].Name+"";
				} else {
					tmp = ""+parameters [i].ParameterType.Name+" "+parameters [i].Name+", ";
				}
				effectiveValue += tmp;
			}
			return effectiveValue;
		}

		public static List<FieldInfoRef> ClassDefaultFields (object obj) {
			List<FieldInfoRef> tmpFieldRefs = new List<FieldInfoRef> ();			
			Type type = obj.GetType ();
			BindingFlags[] flags = ReflectionUtilities.DefaultFlags ();
			FieldInfo[] fields = type.GetFields(flags[0] | flags[1] | flags[2] | BindingFlags.GetField | BindingFlags.SetField);
			for (int i = 0; i < fields.Length; i++) {
				FieldInfoRef tmpFieldRef = new FieldInfoRef (obj, fields[i]);
				if (!tmpFieldRefs.Contains (tmpFieldRef)) {
					tmpFieldRefs.Add (tmpFieldRef);
				}
			}
			return tmpFieldRefs;
		}
	}
}
