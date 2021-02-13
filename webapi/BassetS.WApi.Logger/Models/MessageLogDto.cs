namespace BassetS.WApi.Logger.Models{
    using System;
    public class MessageLogDto{
        public DateTime DT{get;set;}
        //level INTEGER NOT NULL DEFAULT 0,
        public string Source{get;set;}
        public string Message{get;set;}
        public string MessArea {get;set;} 
    }
}