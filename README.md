LibXMLToJSON_CSharp
===================

The library LibXMLToJSON provides a simple XML to JSON converter. Ir produces output that conforms to the conventions introduced by David Calhoun on https://github.com/davidcalhoun/jstoxml .

This library makes use of the free JSON library provided at https://json.codeplex.com/ .

Usage example:

<pre>XmlDocument doc = new XmlDocument();
doc.Load(SOME_FILE_PATH);
JObject jobj = XMLToJSON.ToJSON(doc);</pre>



