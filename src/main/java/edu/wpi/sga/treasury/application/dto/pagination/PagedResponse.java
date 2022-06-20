package edu.wpi.sga.treasury.application.dto.pagination;

import lombok.*;
import org.springframework.data.domain.Page;

import java.time.LocalDateTime;
import java.util.List;
import java.util.function.Function;

@Builder
@Getter
@Setter
@AllArgsConstructor
public class PagedResponse<T> {
    private List<T> data;           // elements
    private String message;

    @Builder.Default
    private LocalDateTime timestamp = LocalDateTime.now();

    private int pageNumber;         // current page number
    private int resultsPerPage;     // number of elements on current page
    private long maxResults;         // total number of elements across all page
    private int totalPages;         // total number of pages
    private int size;               // maximum number of elements per page

    /**
     * Construct a PageResponse containing DTOs given an existing Page of entities and an entity-to-DTO mapper
     *
     * @param page   Page containing entities
     * @param mapper mapper which converts a List of entities to List of DTOs
     * @param <U>    entity class
     */
    public <U> PagedResponse(@NonNull final Page<U> page, @NonNull final Function<List<U>, List<T>> mapper) {
        this.timestamp = LocalDateTime.now();
        this.pageNumber = page.getNumber();
        this.resultsPerPage = page.getNumberOfElements();
        this.maxResults = page.getTotalElements();
        this.totalPages = page.getTotalPages();
        this.size = page.getSize();
        this.data = mapper.apply(page.getContent());
    }
}
