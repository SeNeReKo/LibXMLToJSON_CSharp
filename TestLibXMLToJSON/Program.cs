using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using LibXMLToJSON;


namespace TestLibXMLToJSON
{

	public class Program
	{

		private static readonly string XML_0 = @"<?xml version='1.0' encoding='UTF-8'?>
<ooo>
  <a>
    <foo>bar</foo>
    <foo2>bar2</foo2>
  </a>
  <b>
    <foo a='b'>
      <bar c='d' e='f' />
      <bar g='h'>Some Text</bar>
    </foo>
  </b>
</ooo>".Replace('\'', '\"');

		private static readonly string JSON_0 = @"{
  '_name': 'ooo',
  '_content': [
    {
      '_name': 'a',
      '_content': [
        {
          '_name': 'foo',
          '_content': 'bar'
        },
        {
          '_name': 'foo2',
          '_content': 'bar2'
        }
      ]
    },
    {
      '_name': 'b',
      '_content': [
        {
          '_name': 'foo',
          '_attrs': {
            'a': 'b'
          },
          '_content': [
            {
              '_name': 'bar',
              '_attrs': {
                'c': 'd',
                'e': 'f'
              }
            },
            {
              '_name': 'bar',
              '_attrs': {
                'g': 'h'
              },
              '_content': 'Some Text'
            }
          ]
        }
      ]
    }
  ]
}".Replace('\'', '\"');

		public static void Main(string[] args)
		{
			Console.WriteLine(XML_0);
			Console.WriteLine();
			Console.WriteLine("--------");
			Console.WriteLine();

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(XML_0);
			JObject jobj = XMLToJSON.ToJSON(doc);

			string result = jobj.ToString(Newtonsoft.Json.Formatting.Indented);

			Console.WriteLine(result);
			Console.WriteLine();
			Console.WriteLine("--------");
			Console.WriteLine();

			if (result.Equals(JSON_0)) {
				Console.WriteLine("Test succeeded.");
			} else {
				Console.WriteLine("TEST FAILED!");
			}
		} 

	}

}
