package edu.wpi.sga.treasury.application.enums;

import lombok.Getter;
import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
@Getter
public enum ClubType {
    CAMPUS_WIDE("Campus Wide"),
    CLASSLESS("Classless"),
    CULTURAL("Cultural"),
    GREEK_LIFE("Greek Life"),
    INTERNATIONAL("International"),
    MISC_INTEREST("Misc Interest"),
    PERFORMING_MEDIA("Performing and Media"),
    PROFESSIONAL("Professional"),
    SELECTIVE_MEMBERSHIP("Selective Membership"),
    SOCIETAL_AWARENESS("Societal Awareness"),
    SPORT("Sport"),
    STEM_FOCUSED("STEM Focused");

    private final String name;
}
