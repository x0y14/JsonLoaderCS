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

        public class InvalidParamaterExeception : Exception                                                 
        {                                                                                               
            public InvalidParamaterExeception(String message) : base(message)                               
            { }                                                                                         
                                                                                                
            public InvalidParamaterExeception(String message, Exception inner) : base(message, inner) { }   
        }
        
        public class NotFoundException : Exception                                                 
        {                                                                                               
            public NotFoundException(String message) : base(message)                               
            { }                                                                                         
                                                                                                
            public NotFoundException(String message, Exception inner) : base(message, inner) { }   
        }
    }
}