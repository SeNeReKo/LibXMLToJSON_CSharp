using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


using Newtonsoft.Json.Linq;


namespace LibXMLToJSON
{

	public class XMLToJSON
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		private XMLToJSON()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Converts an XML document to JSON.
		/// </summary>
		/// <param name="xml">The XML document</param>
		/// <returns>Returns a JSON object that represents the same data as the specified XML document.</returns>
		public static JObject ToJSON(XmlDocument xml)
		{
			return ToJSON(xml.DocumentElement);
		}

		/// <summary>
		/// Converts an XML node to a JSON object.
		/// </summary>
		/// <param name="xml">The XML node</param>
		/// <returns>Returns a JSON object that represents the same data as the specified XML node.</returns>
		public static JObject ToJSON(XmlElement xml)
		{
			JObject obj = new JObject();

			obj.Add("_name", new JValue(xml.Name));

			if (xml.Attributes.Count > 0) {
				JObject attribs = new JObject();
				foreach (XmlAttribute a in xml.Attributes) {
					attribs.Add(a.Name, new JValue(a.Value));
				}
				obj.Add("_attrs", attribs);
			}

			{
				bool bContainsText = false;
				bool bContainsElements = false;
				foreach (XmlNode x in xml.ChildNodes) {
					if (x is XmlText) bContainsText = true;
					else
					if (x is XmlEntityReference) bContainsText = true;
					else
					if (x is XmlElement) bContainsElements = true;
				}

				JToken nested;
				if (bContainsElements) {
					JArray a = new JArray();
					foreach (XmlNode x in xml.ChildNodes) {
						if (x is XmlElement) {
							a.Add(ToJSON((XmlElement)x));
						}
					}
					nested = a;
				} else
				if (bContainsText) {
					StringBuilder sb = new StringBuilder();
					foreach (XmlNode x in xml.ChildNodes) {
						if (x is XmlText) {
							sb.Append(x.InnerText);
						} else
						if (x is XmlEntityReference) {
							sb.Append(x.InnerText);
						}
					}
					nested = new JValue(sb.ToString());
				} else {
					nested = null;
				}

				if (nested != null) obj.Add("_content", nested);
			}

			return obj;
		}

	}

}
