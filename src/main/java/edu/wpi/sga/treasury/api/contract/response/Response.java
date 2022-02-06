package edu.wpi.sga.treasury.api.contract.response;

import lombok.*;

import java.time.LocalDateTime;

@Builder
@Getter
@Setter
public class Response<T> {
    private T data;
    private String message;

    @Builder.Default
    private LocalDateTime timestamp = LocalDateTime.now();
}
