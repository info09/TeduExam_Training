﻿namespace Examination.Shared.SeedWork
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }

        public string Message { get; set; }

        public T ResultObj { get; set; }

        public int StatusCode { get; set; }

        public ApiResult()
        {
        }
        public ApiResult(int statusCode, bool isSuccessed, string message = null)
        {
            Message = message;
            IsSuccessed = isSuccessed;
            StatusCode = statusCode;
        }

        public ApiResult(int statusCode, bool isSuccessed, T resultObj, string message = null)
        {
            ResultObj = resultObj;
            Message = message;
            IsSuccessed = isSuccessed;
            StatusCode = statusCode;
        }
    }
}
