﻿using System.Collections.Generic;

namespace Examination.Shared.SeedWork
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public List<string> Errors { get; set; }

        public ApiErrorResult(string message) : base(false, message)
        {

        }

        public ApiErrorResult(List<string> errors) : base(false)
        {
            Errors = errors;
        }
    }
}
