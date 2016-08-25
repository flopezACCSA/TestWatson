using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MiModelo2
/// </summary>
public class MyRequest
{
    public class System
    {
        public List<string> dialog_stack { get; set; }
        public int dialog_turn_counter { get; set; }
        public int dialog_request_counter { get; set; }
    }

    public class Context
    {
        public string conversation_id { get; set; }
        public System system { get; set; }
        public int monto { get; set; }
        public double salario { get; set; }
        public int antiguedad { get; set; }
        public string opcion1 { get; set; }
        public string opcion2 { get; set; }
        public string opcion3 { get; set; }
    }

    public class Input
    {
        public string text { get; set; }
    }

    public class RootObject
    {
        public string workspace_id { get; set; }
        public Context context { get; set; }
        public Input input { get; set; }
        

        public RootObject()
        {
            input = new Input();
            context = new Context();
        
        }
    }
 }