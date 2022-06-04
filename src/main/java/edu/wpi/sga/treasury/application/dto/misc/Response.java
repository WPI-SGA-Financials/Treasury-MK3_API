package edu.wpi.sga.treasury.application.dto.misc;

import lombok.*;
import org.springframework.data.domain.Page;

import java.time.LocalDateTime;
import java.util.List;
import java.util.function.Function;

@Builder
@Getter
@Setter
@AllArgsConstructor
public class Response<T> {
    private T data;
    private String message;

    @Builder.Default
    private LocalDateTime timestamp = LocalDateTime.now();

    public <U> Response(@NonNull final U data, @NonNull final Function<U, T> mapper) {
        this.timestamp = LocalDateTime.now();
        this.data = mapper.apply(data);
    }
}
