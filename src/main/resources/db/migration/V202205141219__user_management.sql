create table user
(
    id            int auto_increment primary key,
    first_name    varchar(100) not null,
    last_name     varchar(100) not null,
    role          varchar(10)  not null,
    working_level varchar(10)  not null,
    msal_id       varchar(255) not null,
    constraint user_msal_id_uq unique (msal_id)
);

create table organization_user
(
    id              int auto_increment primary key,
    user_id         int not null,
    organization_id int not null,
    constraint organization_user_user_id_fk
        foreign key (user_id) references user (id)
            on update cascade on delete cascade,
    constraint organization_user_organization_id_fk
        foreign key (organization_id) references organization (id)
            on update cascade on delete cascade
)