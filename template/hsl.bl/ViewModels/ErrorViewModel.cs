namespace hsl.api.Models
{
    public class ErrorViewModel
    {
        public int code { get; set; }
        public string tag { get; set; }
        public string caption { get; set; }
        public string afterAction { get; set; }
    }

//TODO: enum
//    enum ErrorTag
//    {
//        notFoundError = 1,
//        exceptionError, 
//    }
}