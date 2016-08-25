using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class WatsonResponse
{

    public class Input
    {
        public string text { get; set; }
    }

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
        public double monto { get; set; }
        public double salario { get; set; }
        public int antiguedad { get; set; }
        public string opcion1 { get; set; }
        public string opcion2 { get; set; }
        public string opcion3 { get; set; }
    }

    public class Intent
    {
        public string intent { get; set; }
        public double confidence { get; set; }
    }

    public class Output
    {
        public List<object> log_messages { get; set; }
        public List<string> text { get; set; }
        public List<string> nodes_visited { get; set; }
    }

    public class RootObject
    {
        public Input input { get; set; }
        public bool alternate_intents { get; set; }
        public Context context { get; set; }
        public List<object> entities { get; set; }
        public List<Intent> intents { get; set; }
        public Output output { get; set; }
        public string workspace_id { get; set; }
        public System system { get; set; }

        public RootObject()
        {
            input = new Input();
            context = new Context();
            entities = new List<object>();
            intents = new List<Intent>();
            output = new Output();
            system = new System();
        }
    }
}