# Organization Tables
create table if not exists organization
(
    id             integer auto_increment primary key,
    name_of_club   varchar(255)                        not null,
    classification varchar(100)                        not null,
    type_of_club   varchar(100)                        null,
    account_number varchar(8)                          null,
    acronym        varchar(50)                         null,
    is_inactive    bit                                 not null,
    last_modified  timestamp default CURRENT_TIMESTAMP not null,
    constraint unique name_of_club_unique (name_of_club)
);

create table if not exists `club_classification`
(
    id              integer auto_increment primary key,
    organization_id integer      not null,
    category        varchar(255) not null,
    constraint club_classification_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
);

create table if not exists techsync
(
    id              integer auto_increment primary key,
    organization_id integer                             not null,
    techsync_name   varchar(255)                        null,
    last_modified   timestamp default CURRENT_TIMESTAMP not null,
    constraint techsync_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
);

create table if not exists organization_comment
(
    id              integer auto_increment primary key,
    organization_id integer                             not null,
    comment_date    date                                not null,
    comment         varchar(255)                        not null,
    last_modified   timestamp default CURRENT_TIMESTAMP not null,
    constraint organization_comment_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
);

create table if not exists organization_contact_info
(
    id              integer auto_increment primary key,
    organization_id integer                             not null,
    president_email varchar(255)                        null,
    treasure_email  varchar(255)                        null,
    last_modified   timestamp default CURRENT_TIMESTAMP not null,
    constraint organization_contact_info_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
);

create table if not exists organization_membership_number
(
    id                  integer auto_increment primary key,
    organization_id     integer                                not null,
    fiscal_year         varchar(255)                           not null,
    amount_member_count varchar(255) default 'Not Provided'    not null,
    last_modified       timestamp    default CURRENT_TIMESTAMP not null,
    constraint organization_membership_number_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
);

# Budget Tables
create table if not exists budget
(
    id              integer auto_increment primary key,
    organization_id integer                                not null,
    fiscal_year     varchar(6)                             not null,
    notes           varchar(255) default ''                null,
    last_modified   timestamp    default CURRENT_TIMESTAMP not null,
    constraint budget_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
);

create table if not exists budget_legacy
(
    id               integer auto_increment primary key,
    budget_id        integer                                  not null,
    amount_requested decimal(10, 2)                           not null,
    amount_proposed  decimal(10, 2)                           not null,
    appealed         bit            default b'0'              not null,
    amount_appealed  decimal(10, 2) default 0.00              not null,
    appeal_decision  varchar(20)    default ''                not null,
    approved_appeal  decimal(10, 2) default 0.00              not null,
    amount_spent     decimal(10, 2) default 0.00              not null,
    last_modified    timestamp      default CURRENT_TIMESTAMP not null,
    constraint budget_legacy_budget_id_fk
        foreign key (budget_id) references budget (id)
            on update cascade on delete cascade
);

create table if not exists budget_section
(
    id            integer auto_increment primary key,
    budget_id     integer                             not null,
    section_name  varchar(255)                        not null,
    last_modified timestamp default CURRENT_TIMESTAMP not null,
    constraint budget_section_budget_id_fk
        foreign key (budget_id) references budget (id)
            on update cascade on delete cascade
);

create table if not exists budget_line_item
(
    id                integer auto_increment primary key,
    budget_section_id integer                                  not null,
    line_item_name    varchar(255)                             not null,
    amount_requested  decimal(10, 2)                           not null,
    amount_proposed   decimal(10, 2)                           not null,
    appealed          bit            default b'0'              not null,
    amount_appealed   decimal(10, 2) default 0.00              not null,
    appeal_decision   varchar(20)    default ''                not null,
    approved_appeal   decimal(10, 2) default 0.00              not null,
    amount_spent      decimal(10, 2) default 0.00              not null,
    notes             varchar(255)   default ''                not null,
    last_modified     timestamp      default CURRENT_TIMESTAMP not null,
    constraint budget_line_item_budget_section_id_fk
        foreign key (budget_section_id) references budget_section (id)
            on update cascade on delete cascade
);

# Funding Request Tables
create table if not exists funding_request
(
    id               integer auto_increment primary key,
    organization_id  integer                             not null,
    description      varchar(255)                        null,
    hearing_date     date                                not null,
    fiscal_year      varchar(6)                          null,
    date_of_event    date                                null,
    dot_number       varchar(6)                          not null,
    amount_requested decimal(10, 2)                      not null,
    decision         varchar(20)                         not null,
    amount_approved  decimal(10, 2)                      not null,
    notes            varchar(512)                        null,
    last_modified    timestamp default CURRENT_TIMESTAMP not null,
    constraint funding_request_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
);

create table if not exists fr_appeal
(
    id                 integer auto_increment primary key,
    funding_request_id integer                             not null,
    new_dot_number     varchar(6)                          not null,
    appeal_date        date                                not null,
    description        varchar(255)                        null,
    appeal_amount      decimal(10, 2)                      not null,
    decision           varchar(20)                         not null,
    approved_appeal    decimal(10, 2)                      not null,
    notes              varchar(255)                        null,
    minutes_link       varchar(255)                        null,
    last_modified      timestamp default CURRENT_TIMESTAMP not null,
    constraint fr_appeal_funding_request_id_fk
        foreign key (funding_request_id) references funding_request (id)
            on update cascade on delete cascade
);

create table if not exists fr_minute
(
    id                 integer auto_increment primary key,
    funding_request_id integer                             not null,
    agenda_number      varchar(9)                          not null,
    minutes_link       varchar(255)                        null,
    last_modified      timestamp default CURRENT_TIMESTAMP not null,
    constraint funding_request_id_unique
        unique (funding_request_id),
    constraint fr_minute_funding_request_id_fk
        foreign key (funding_request_id) references funding_request (id)
            on update cascade on delete cascade
);

create table if not exists fr_report_form
(
    id                 integer auto_increment primary key,
    funding_request_id integer                             not null,
    amount_spent       decimal(10, 2)                      null,
    status             varchar(25)                         null,
    amount_approved    decimal(10, 2)                      null,
    approved_date      date                                null,
    notes              varchar(255)                        null,
    last_modified      timestamp default CURRENT_TIMESTAMP not null,
    constraint funding_request_id_unique
        unique (funding_request_id),
    constraint fr_report_form_funding_request_id_fk
        foreign key (funding_request_id) references funding_request (id)
            on update cascade on delete cascade
);

create table if not exists fr_supplemental
(
    id                 integer auto_increment primary key,
    funding_request_id integer                             not null,
    item_type          varchar(100)                        not null,
    other_type         varchar(100)                        null,
    amount_requested   decimal(10, 2)                      not null,
    amended            bit                                 null,
    amended_amount     decimal(10, 2)                      null,
    notes              varchar(255)                        null,
    last_modified      timestamp default CURRENT_TIMESTAMP null,
    constraint fr_supplemental_funding_request_id_fk
        foreign key (funding_request_id) references funding_request (id)
            on update cascade on delete cascade
);

create table if not exists fr_workday_idt
(
    id                    integer auto_increment primary key,
    funding_request_id    integer                             not null,
    idt_submitted         bit       default b'0'              null,
    workday_approved      varchar(15)                         null,
    workday_approval_date date                                null,
    last_modified         timestamp default CURRENT_TIMESTAMP not null,
    constraint funding_request_id_unique
        unique (funding_request_id),
    constraint fr_workday_idt_funding_request_id_fk
        foreign key (funding_request_id) references funding_request (id)
            on update cascade on delete cascade
);

# Reallocation Request Tables
create table if not exists reallocation
(
    id               int auto_increment primary key,
    organization_id  integer                             not null,
    description      varchar(255)                        not null,
    hearing_date     date                                not null,
    fiscal_year      varchar(6)                          not null,
    dot_number       varchar(6)                          not null,
    allocated_from   varchar(255)                        null,
    allocated_to     varchar(255)                        null,
    amount_allocated decimal(10, 2)                      not null,
    decision         varchar(255)                        not null,
    amount_approved  decimal(10, 2)                      not null,
    notes            varchar(255)                        null,
    last_modified    timestamp default CURRENT_TIMESTAMP not null,
    constraint reallocation_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
);

create table if not exists reallocation_minute
(
    id              int auto_increment primary key,
    reallocation_id int                                 not null,
    agenda_number   varchar(9)                          not null,
    minutes_link    varchar(255)                        not null,
    last_modified   timestamp default CURRENT_TIMESTAMP not null,
    constraint reallocation_minute_reallocation_id_fk
        foreign key (reallocation_id) references reallocation (id)
            on update cascade on delete cascade
);

# Reclassification Request Tables
create table if not exists reclassification
(
    id              integer auto_increment primary key,
    organization_id integer                             not null,
    hearing_date    date                                not null,
    fiscal_year     varchar(255)                        not null,
    dot_number      varchar(255)                        not null,
    original_class  varchar(255)                        not null,
    requested_class varchar(255)                        not null,
    decision        varchar(255)                        not null,
    approved_class  varchar(255)                        not null,
    notes           varchar(255)                        null,
    last_modified   timestamp default CURRENT_TIMESTAMP not null,
    constraint reclassification_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
);

create table if not exists reclassification_minute
(
    id                  integer auto_increment primary key,
    reclassification_id integer                             not null,
    agenda_number       varchar(9)                          not null,
    minutes_link        varchar(255)                        not null,
    last_modified       timestamp default CURRENT_TIMESTAMP null,
    constraint reclassification_minute_reclassification_id_fk
        foreign key (reclassification_id) references reclassification (id)
            on update cascade on delete cascade
);

# Mandatory Transfer Tables
create table if not exists mandatory_transfer
(
    id               integer auto_increment primary key,
    organization_id  integer                             not null,
    fund_name        varchar(255)                        not null,
    fiscal_year      varchar(255)                        not null,
    worktag          varchar(255)                        not null,
    amount_requested decimal(10, 2)                      not null,
    amount_proposed  decimal(10, 2)                      not null,
    amount_approved  decimal(10, 2)                      not null,
    notes            varchar(255)                        null,
    last_modified    timestamp default CURRENT_TIMESTAMP not null,
    constraint mandatory_transfer_organization_id_fk
        foreign key (organization_id) references organization (id)
);

create table if not exists mandatory_transfer_line_item
(
    id                    integer auto_increment primary key,
    mandatory_transfer_id integer                             not null,
    line_item_name        varchar(255)                        not null,
    amount                decimal(10, 2)                      not null,
    notes                 varchar(255)                        null,
    last_modified         timestamp default CURRENT_TIMESTAMP not null,
    constraint mandatory_transfer_line_item_mandatory_transfer_id_fk
        foreign key (mandatory_transfer_id) references mandatory_transfer (id)
            on update cascade on delete cascade
);

create table if not exists operating_expense
(
    id                              integer auto_increment primary key,
    mandatory_transfer_line_item_id integer                             not null,
    amount_spent                    decimal(10, 2)                      not null,
    person                          varchar(100)                        not null,
    description                     varchar(255)                        not null,
    payment_type                    varchar(20)                         not null,
    workday_approved                bit                                 null,
    workday_approval_date           date                                null,
    notes                           varchar(255)                        null,
    last_modified                   timestamp default CURRENT_TIMESTAMP not null,
    constraint operating_expense_mandatory_transfer_line_item_id_fk
        foreign key (mandatory_transfer_line_item_id) references mandatory_transfer_line_item (id)
            on update cascade on delete cascade
);

# Misc Tables
create table if not exists funding_account
(
    id              integer auto_increment primary key,
    account_name    varchar(255)                        not null,
    fiscal_year     varchar(255)                        not null,
    fall_transfer   decimal(10, 2)                      null,
    spring_transfer decimal(10, 2)                      null,
    worktag         varchar(8)                          not null,
    last_modified   timestamp default CURRENT_TIMESTAMP not null
);

create table if not exists transfer
(
    id                   integer auto_increment primary key,
    fiscal_year          varchar(255)                        not null,
    funding_account_from integer                             not null,
    funding_account_to   integer                             not null,
    amount               decimal(10, 2)                      null,
    transfer_date        date                                not null,
    notes                varchar(255)                        null,
    last_modified        timestamp default CURRENT_TIMESTAMP not null,
    constraint transfer_funding_account_from_fk
        foreign key (funding_account_from) references funding_account (id),
    constraint transfer_funding_account_to_fk
        foreign key (funding_account_to) references funding_account (id)
);

create table if not exists student_life_fee
(
    id                      int auto_increment primary key,
    fiscal_year             varchar(6)                          not null,
    student_life_fee_amount decimal(10, 2)                      not null,
    fall_student_amount     int                                 null,
    last_modified           timestamp default CURRENT_TIMESTAMP not null
);

create table if not exists student_organization_council
(
    id                       int auto_increment primary key,
    name_of_club             varchar(255)                        not null,
    acronym                  varchar(255)                        null,
    hearing_date             date                                not null,
    fiscal_year              varchar(255)                        not null,
    type_of_name             varchar(255)                        not null,
    president_email          varchar(255)                        null,
    treasurer_email          varchar(255)                        null,
    projected_active_members int                                 null,
    decision                 varchar(255)                        not null,
    notes                    varchar(255)                        null,
    last_modified            timestamp default CURRENT_TIMESTAMP not null
);

# Create Functions
Create Function fnc_fiscal_class(amt_proposed DECIMAL(10, 2), amt_appeal_approved DECIMAL(10, 2))
    returns VARCHAR(15)
    DETERMINISTIC
BEGIN
    DECLARE class varchar(15);
    DECLARE amt_approved DECIMAL(10, 2);
    Set amt_approved = amt_proposed + amt_appeal_approved;

    if amt_approved > 0 and amt_approved < 1000 then # Between $0 and $1,000
        Set class = 'Class A';
    elseif amt_approved >= 1000 and amt_approved < 5000 then # Between $1,000 and $5,000
        Set class = 'Class B';
    elseif amt_approved >= 5000 and amt_approved < 10000 then # Between $5,000 and $10,000
        Set class = 'Class C';
    elseif amt_approved >= 10000 and amt_approved < 50000 then # Between $10,000 and $50,000
        Set class = 'Class D';
    elseif amt_approved >= 50000 and amt_approved < 100000 then # Between $50,000 and $100,000
        Set class = 'Class E';
    elseif amt_approved >= 100000 then # Greater than $100,000
        Set class = 'Class F';
    else
        Set class = 'Not Budgeted';
    end if;

    return class;
end;

Create Function fnc_fiscal_expenditure_grading(amt_proposed DECIMAL(10, 2), amt_appeal_approved DECIMAL(10, 2),
                                               amt_spent DECIMAL(10, 2))
    returns VARCHAR(8)
    DETERMINISTIC
BEGIN
    DECLARE class varchar(15);
    DECLARE ratio DECIMAL(10, 2);
    DECLARE grade varchar(8);

    if amt_proposed + amt_appeal_approved = 0 Then
        Set ratio = 0;
    else
        Set ratio = amt_spent / (amt_proposed + amt_appeal_approved);
    end if;

    Set class = fnc_fiscal_class(amt_proposed, amt_appeal_approved);

    if class = 'Class A' then
        If ratio > 0.85 and ratio <= 1.0 Then
            Set grade = 'A';
        elseif ratio > 0.70 and ratio <= 0.85 Then
            Set grade = 'B';
        elseif ratio > 0.55 and ratio <= 0.70 Then
            Set grade = 'C';
        else
            Set grade = 'NR';
        end if;
    elseif class = 'Class B' then
        If ratio > 0.90 and ratio <= 1.0 Then
            Set grade = 'A';
        elseif ratio > 0.75 and ratio <= 0.90 Then
            Set grade = 'B';
        elseif ratio > 0.60 and ratio <= 0.75 Then
            Set grade = 'C';
        else
            Set grade = 'NR';
        end if;
    elseif class = 'Class C' then
        If ratio > 0.90 and ratio <= 1.0 Then
            Set grade = 'A';
        elseif ratio > 0.80 and ratio <= 0.90 Then
            Set grade = 'B';
        elseif ratio > 0.70 and ratio <= 0.80 Then
            Set grade = 'C';
        else
            Set grade = 'NR';
        end if;
    elseif class = 'Class D' then
        If ratio > 0.95 and ratio <= 1.0 Then
            Set grade = 'A';
        elseif ratio > 0.85 and ratio <= 0.95 Then
            Set grade = 'B';
        elseif ratio > 0.75 and ratio <= 0.85 Then
            Set grade = 'C';
        else
            Set grade = 'NR';
        end if;
    elseif class = 'Class E' then
        If ratio > 0.95 and ratio <= 1.0 Then
            Set grade = 'A';
        elseif ratio > 0.85 and ratio <= 0.95 Then
            Set grade = 'B';
        elseif ratio > 0.80 and ratio <= 0.85 Then
            Set grade = 'C';
        else
            Set grade = 'NR';
        end if;
    elseif class = 'Class F' then
        If ratio > 0.95 and ratio <= 1.0 Then
            Set grade = 'A';
        elseif ratio > 0.90 and ratio <= 0.95 Then
            Set grade = 'B';
        elseif ratio > 0.85 and ratio <= 0.90 Then
            Set grade = 'C';
        else
            Set grade = 'NR';
        end if;
    else
        Set grade = 'No Grade';
    end if;

    return grade;
end;

Create Function fnc_fiscal_year(hearing_date DATE)
    returns VARCHAR(5)
    DETERMINISTIC
BEGIN
    DECLARE fiscal_year varchar(15);

    if Month(hearing_date) < 7 then
        Set fiscal_year = CONCAT('FY ', RIGHT(Year(hearing_date), 2));
    else
        Set fiscal_year = Concat('FY ', RIGHT(Year(hearing_date), 2) + 1);
    end if;

    return fiscal_year;
end;

Create Function fnc_find_months(hearing_date DATE)
    returns integer
    DETERMINISTIC
BEGIN
    return (Month(CURDATE()) - Month(hearing_date)) + (Year(CURDATE()) - Year(hearing_date)) * 12;
end;

# Create Views
create or replace view `all_operating_expenses` as
select fiscal_year,
       line_item_name,
       amount_spent,
       person,
       description,
       payment_type,
       workday_approved,
       workday_approval_date,
       oe.notes
from mandatory_transfer_line_item
         join operating_expense oe on mandatory_transfer_line_item.id = oe.mandatory_transfer_line_item_id
         left join mandatory_transfer mt on mandatory_transfer_line_item.mandatory_transfer_id = mt.id
order by fiscal_year desc;

CREATE or replace VIEW complete_funding_request AS
select fr.id,
       name_of_club,
       hearing_date,
       fnc_fiscal_year(hearing_date)                          as fiscal_year,
       agenda_number,
       dot_number,
       fr.description,
       date_of_event,
       amount_requested,
       fr.decision,
       fr.amount_approved,
       fr.notes,
       fm.minutes_link,
       if(isnull(fa.funding_request_id), 'No', 'Yes')         as appealed,
       if(isnull(fa.funding_request_id), 0, appeal_amount)    as appeal_amount,
       if(isnull(fa.funding_request_id), '', fa.decision)     as appeal_decision,
       if(isnull(fa.funding_request_id), 0, approved_appeal)  as approved_appeal,
       if(isnull(fa.funding_request_id), '', fa.minutes_link) as appeal_minutes_link,
       amount_spent,
       status                                                 as report_form_status,
       frf.amount_approved                                    as report_form_approved,
       idt_submitted,
       workday_approved,
       workday_approval_date
from funding_request fr
         inner join organization o on fr.organization_id = o.id
         left join fr_report_form frf on fr.id = frf.funding_request_id
         left join fr_workday_idt fwi on fr.id = fwi.funding_request_id
         left join fr_appeal fa on fr.id = fa.funding_request_id
         left join fr_minute fm on fr.id = fm.funding_request_id
order by hearing_date desc, dot_number desc;

CREATE or replace VIEW all_requests AS
select name_of_club,
       hearing_date,
       fiscal_year,
       agenda_number,
       dot_number,
       description,
       date_of_event,
       amount_requested,
       decision,
       amount_approved,
       notes,
       minutes_link,
       appealed,
       appeal_amount,
       appeal_decision,
       approved_appeal,
       appeal_minutes_link,
       amount_spent,
       report_form_status,
       report_form_approved,
       idt_submitted,
       workday_approved,
       workday_approval_date
from complete_funding_request

union

select name_of_club,
       hearing_date,
       fnc_fiscal_year(hearing_date) AS fiscal_year,
       agenda_number,
       dot_number,
       'Reclassification',
       NULL,
       requested_class,
       decision,
       approved_class,
       notes,
       minutes_link,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL
from reclassification r
         left join reclassification_minute rm on r.id = rm.reclassification_id
         inner join organization o on o.id = r.organization_id

union

select name_of_club,
       hearing_date,
       fnc_fiscal_year(hearing_date),
       agenda_number,
       dot_number,
       concat('Reallocation: ', r.description),
       NULL,
       amount_allocated,
       r.decision,
       amount_approved,
       r.notes,
       minutes_link,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL,
       NULL
from reallocation r
         left join `reallocation_minute` rm on r.id = rm.reallocation_id
         inner join organization o on r.organization_id = o.id
order by hearing_date desc, dot_number desc;

CREATE or replace VIEW budget_by_section AS
select organization_id,
       name_of_club,
       fiscal_year,
       section_name,
       count(line_item_name) AS `num_of_items`,
       sum(amount_requested) AS `amount_requested`,
       sum(amount_proposed)  AS amount_proposed,
       sum(approved_appeal)  AS approved_appeal,
       sum(amount_spent)     AS amount_spent
from budget b
         inner join organization o on b.organization_id = o.id
         inner join budget_section bs on b.id = bs.budget_id
         inner join budget_line_item bli on bs.id = bli.budget_section_id
group by name_of_club, fiscal_year, section_name
order by name_of_club, fiscal_year;

CREATE or replace VIEW budget_by_fy AS
select organization_id,
       name_of_club,
       fiscal_year,
       sum(num_of_items)                      AS num_of_items,
       sum(amount_requested)                  AS amount_requested,
       sum(amount_proposed)                   AS amount_proposed,
       sum(approved_appeal)                   AS approved_appeal,
       sum(approved_appeal + amount_proposed) AS amount_approved,
       sum(amount_spent)                      AS amount_spent
from budget_by_section
group by budget_by_section.name_of_club, budget_by_section.fiscal_year

union

select organization_id,
       name_of_club,
       fiscal_year,
       -1,
       amount_requested,
       amount_proposed,
       approved_appeal,
       sum(approved_appeal + amount_proposed),
       amount_spent
from budget b
         inner join budget_legacy bl on b.id = bl.budget_id
         inner join organization o on b.organization_id = o.id
group by name_of_club, fiscal_year
order by name_of_club, fiscal_year;

CREATE or replace VIEW budget_line_item_readable AS
select name_of_club,
       fiscal_year,
       section_name,
       line_item_name,
       amount_requested,
       amount_proposed,
       approved_appeal,
       amount_spent
from budget b
         inner join organization o on b.organization_id = o.id
         inner join budget_section bs on b.id = bs.budget_id
         inner join budget_line_item bli on bs.id = bli.budget_section_id
group by name_of_club, fiscal_year, section_name, line_item_name
order by name_of_club, fiscal_year;

CREATE or replace VIEW `categories_club_membership` AS
select fiscal_year,
       category,
       amount_member_count
from organization_membership_number omn
         inner join club_classification cc on omn.organization_id = cc.organization_id
order by fiscal_year desc;

CREATE or replace VIEW club_total_budget AS
select fiscal_year,
       name_of_club,
       category,
       amount_approved
from budget_by_fy
         left join club_classification cc on budget_by_fy.organization_id = cc.organization_id
group by name_of_club, fiscal_year
order by name_of_club, fiscal_year desc;

CREATE or replace VIEW categories_total_budget AS
select fiscal_year,
       category,
       sum(amount_approved) AS `total`
from club_total_budget
group by fiscal_year, category
order by fiscal_year;

CREATE or replace VIEW financial_transparency AS
select organization.name_of_club,
       classification,
       if((isnull(amount_member_count) or amount_member_count = ''), 'Not Provided',
          amount_member_count)                                         as active_members,
       if(isnull(sum(fr.amount_approved)), 0, sum(fr.amount_approved)) AS funding_request_amount,
       if(isnull(bbf.amount_approved), 0, bbf.amount_approved)         AS budget_amount
from organization
         left join organization_membership_number omn
                   on organization.id = omn.organization_id and omn.fiscal_year = 'FY 20'
         left join budget_by_fy bbf on organization.id = bbf.organization_id and bbf.fiscal_year = 'FY 20'
         left join funding_request fr on organization.id = fr.organization_id and fr.fiscal_year = 'FY 20'
where is_inactive = 0
  and organization.classification not in ('Department', 'Graduate', 'Mandatory Transfer')
group by organization.name_of_club;

CREATE or replace VIEW fiscal_expenditure_grades AS
select b.id                                                         as budget_id,
       name_of_club,
       fiscal_year,
       type_of_club,
       classification,
       fnc_fiscal_class(sum(amount_proposed), sum(approved_appeal)) AS fiscal_class,
       count(distinct section_name)                                 AS number_of_sections,
       count(section_name)                                          AS number_of_items,
       sum((bli.approved_appeal + bli.amount_proposed))             AS amount_approved,
       sum(bli.amount_spent)                                        AS amount_spent,
       fnc_fiscal_expenditure_grading(sum(bli.amount_proposed), sum(bli.approved_appeal),
                                      sum(bli.amount_spent))        AS grade
from budget b
         inner join organization o on b.organization_id = o.id
         inner join budget_section bs on b.id = bs.budget_id
         inner join budget_line_item bli on bs.id = bli.budget_section_id
group by name_of_club, fiscal_year
order by name_of_club, fiscal_year;

CREATE or replace VIEW `liability` AS
select fiscal_year,
       count(fr.id)                                                          AS heard_requests,
       sum(fr.amount_approved)                                               AS total_approved,
       sum(frf.amount_approved)                                              AS report_form_approved_amount,
       sum(if(status = 'Approved', frf.amount_approved, fr.amount_approved)) AS total_liability,
       sum(if(workday_approved = 'Yes', 1, 0))                               AS workday_approved_requests,
       sum(if(workday_approved = 'Yes', 0, if(status = 'Approved', frf.amount_approved,
                                              fr.amount_approved)))          AS total_workday_liability
from funding_request fr
         left join fr_report_form frf on fr.id = frf.funding_request_id
         left join fr_workday_idt fwi on fr.id = fwi.funding_request_id
where fr.amount_approved > 0
group by fiscal_year;

CREATE or replace VIEW mandatory_transfer_total_budget AS
select fiscal_year,
       name_of_club,
       fund_name,
       amount_approved
from mandatory_transfer
         inner join organization o on mandatory_transfer.organization_id = o.id
order by fiscal_year desc;

CREATE or replace VIEW operating_account AS
select mtli.id,
       line_item_name,
       amount,
       if(isnull(sum(amount_spent)), 0, sum(amount_spent))               AS amount_spent,
       mtli.amount - if(isnull(sum(amount_spent)), 0, sum(amount_spent)) AS remaining,
       count(oe.id)                                                      AS charges
from mandatory_transfer
         inner join organization o on mandatory_transfer.organization_id = o.id
         inner join mandatory_transfer_line_item mtli on mandatory_transfer.id = mtli.mandatory_transfer_id
         inner join operating_expense oe on mtli.id = oe.mandatory_transfer_line_item_id
where fund_name = 'Operating Account'
  and name_of_club = 'Student Government Association'
  and fiscal_year = fnc_fiscal_year(now())
group by line_item_name;

CREATE or replace VIEW total_life_fee_budget AS
select fiscal_year,
       (student_life_fee_amount * fall_student_amount) AS total
from student_life_fee
order by fiscal_year desc;

CREATE or replace VIEW yearly_club_total_budget AS
select fiscal_year,
       sum(amount_approved) AS total
from club_total_budget
group by fiscal_year
order by fiscal_year desc;

CREATE or replace VIEW yearly_mandatory_transfers_total_budget AS
select fiscal_year,
       sum(amount_approved) AS total
from mandatory_transfer_total_budget
group by fiscal_year
order by fiscal_year desc;

CREATE or replace VIEW total_budget AS
select tlfb.fiscal_year                          AS fiscal_year,
       tlfb.total,
       yctb.total                                AS club_budget,
       ymttb.total                               AS mandatory_transfer_budget,
       ((tlfb.total - yctb.total) - ymttb.total) AS other
from total_life_fee_budget tlfb
         join yearly_club_total_budget yctb on tlfb.fiscal_year = yctb.fiscal_year
         join yearly_mandatory_transfers_total_budget ymttb on tlfb.fiscal_year = ymttb.fiscal_year
order by fiscal_year desc;

CREATE or replace VIEW other_budget AS
select tb.fiscal_year,
       other,
       total_liability,
       other - l.total_liability AS sponsorship_rollback
from total_budget tb
         inner join liability l on tb.fiscal_year = l.fiscal_year
order by fiscal_year desc;

CREATE or replace VIEW overall_numbers AS
select 'Allocated Budget' AS `Title`, sum((bli.amount_proposed + bli.approved_appeal)) AS `Amount`
from budget b
         join budget_section bs on b.id = bs.budget_id
         join budget_line_item bli on bs.id = bli.budget_section_id
where b.fiscal_year like '%21'

union

select 'Allocated FR', sum(if(report_form_status = 'Approved', report_form_approved, amount_approved))
from complete_funding_request
where complete_funding_request.fiscal_year like '%21'

union

select 'Allocated MT', sum(amount_approved)
from mandatory_transfer
where mandatory_transfer.fiscal_year like '%21'

union

select 'Active Clubs', sum(if(is_inactive = 0, 1, 0))
from organization

union

select 'Inactive Clubs', sum(if(is_inactive = 1, 1, 0))
from organization;

CREATE or replace VIEW selection_options AS
select ctb.fiscal_year,
       ctb.name_of_club,
       ctb.category,
       ctb.amount_approved,
       omn.amount_member_count
from club_total_budget ctb
         inner join organization o on ctb.name_of_club = o.name_of_club
         inner join organization_membership_number omn on o.id = omn.organization_id and
                                                          ctb.fiscal_year = omn.fiscal_year
order by category;
