package edu.wpi.sga.treasury.api.contract.response;

import lombok.*;

@Builder
@Getter
@Setter
public class Response<T> {
    private T data;
    private String message;
}
