using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace DNWS
{
    public class Cookie{

        protected static Dictionary<string,string> _cookieDictionary;
        protected static Dictionary<string,string> _setcookieDictionary;
        private string get_session(){
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars,32).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Cookie(string cookiestring = null){

            _cookieDictionary = new Dictionary<string, string>();
            _setcookieDictionary = new Dictionary<string, string>();

            if(cookiestring != null){
                string[] cookieList = Regex.Split(cookiestring,"; "); 

                foreach(string cookie in cookieList){
                    string[] prase = Regex.Split(cookie,"=");
                    if(prase.Length == 2){
                        if(_cookieDictionary.ContainsKey(prase[0]) == false){
                            _cookieDictionary.Add(prase[0],prase[1]);
                        }else{
                            _cookieDictionary[prase[0]] = prase[1];
                        }
                    }
                }
            }
            
            if(Get("_SESSION") == null){
                Set("_SESSION",get_session());
            }
        }

        public void Set(string key,string value)
        {
            if(_cookieDictionary.ContainsKey(key) == false){
                _cookieDictionary.Add(key,value);
            }else{
                _cookieDictionary[key] = value;
            }           

            if(_setcookieDictionary.ContainsKey(key) == false){
                _setcookieDictionary.Add(key,value);
            }else{
                _setcookieDictionary[key] = value;
            } 

        } 

        public string Get(string key){
            if(_cookieDictionary.ContainsKey(key) == true){
                return _cookieDictionary[key];
            }else{
                return null;
            }
        }

        public string setString(){

            string query = "";

            foreach (var pair in _setcookieDictionary)
            {
                if(query.Length == 0){
                    query = pair.Key+"="+pair.Value;
                }else{
                    query = query+";"+pair.Key+"="+pair.Value;
                }
            }

            return query;
        }

    }
}