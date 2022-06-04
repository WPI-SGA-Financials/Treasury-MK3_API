package edu.wpi.sga.treasury.application.dto.misc;

import lombok.*;

import java.time.LocalDateTime;
import java.util.List;
import java.util.function.Function;

@Builder
@Getter
@Setter
@AllArgsConstructor
public class ListResponse<T> {
    private List<T> data;
    private String message;

    @Builder.Default
    private LocalDateTime timestamp = LocalDateTime.now();

    public <U> ListResponse(@NonNull final List<U> data, @NonNull final Function<List<U>, List<T>> mapper) {
        this.timestamp = LocalDateTime.now();
        this.data = mapper.apply(data);
    }
}
