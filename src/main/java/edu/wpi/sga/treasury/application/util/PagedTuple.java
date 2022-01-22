package edu.wpi.sga.treasury.application.util;

import lombok.Getter;
import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
@Getter
public class PagedTuple<K, V> {
    private final K data;
    private final V maxResults;
}
