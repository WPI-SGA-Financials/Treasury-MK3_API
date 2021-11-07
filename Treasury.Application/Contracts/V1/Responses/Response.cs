﻿namespace Treasury.Application.Contracts.V1.Responses
{
    public class Response<T>
    {
        public Response(){}

        public Response(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public string Message { get; set; }
    }
}