package edu.wpi.sga.treasury.application.enums;

import lombok.Getter;
import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
@Getter
public enum ClubClassification {

    CLASS_ONE("Class 1 - Special Interest"),
    CLASS_TWO("Class 2 - Club Sports"),
    CLASS_THREE("Class 3 - Campus Wide"),
    CLASS_FOUR("Class 4 - Selective Membership"),
    CLASS_FIVE("Class 5 - Greek Life"),
    CLASS_SIX("Class 6 - Provisional"),
    CLASS_SEVEN("Class 7 - Non-Funded"),
    CLASS_EIGHT("Class 8 - Student Run Business"),
    CLASSLESS("Classless"),
    DEPARTMENT("Department"),
    GRADUATE("Graduate"),
    MANDATORY_TRANSFER("Mandatory Transfer");

    private final String name;
}
