namespace TISMonitor.TISWebService
{
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [CollectionDataContract(Name="ArrayOfString", Namespace="http://tempuri.org/", ItemName="string"), DebuggerStepThrough, GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    public class ArrayOfString : ObservableCollection<string>
    {
    }
}

