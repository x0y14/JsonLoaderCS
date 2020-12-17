using System;
namespace JsonLoaderCS
{
    public class Errors
    {
        public class InvalidSyntaxException : Exception
        {
            public InvalidSyntaxException(String message) : base(message)
            { }

            public InvalidSyntaxException(String message, Exception inner) : base(message, inner) { }
        }

        public class InvalidParamaterException : Exception                                                 
        {
            public InvalidParamaterException(String message) : base(message)
            { }                                                                                         
                                                                                                
            public InvalidParamaterException(String message, Exception inner) : base(message, inner) { }   
        }
        
        public class NotFoundException : Exception                                                 
        {
            public NotFoundException(string message) : base(message)
            { }                                                                                         
                                                                                                
            public NotFoundException(string message, Exception inner) : base(message, inner) { }   
        }

        public static string ErrorMessageMaker(string message, string filename, string method, (string, string, string) nears, int lenght, int pos)
        {
            return $" < {filename}::{method} >({pos} / {lenght}) => {nears}\n| {message}";
        }
    }
}