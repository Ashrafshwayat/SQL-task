create table city(
id int identity(1,1) primary key,
city_name varchar(50),
);
create table customers(
id int identity(1,1),
city_id int,
name varchar(50),
age int,
image varchar(255),
foreign key (city_id) references city,

);

insert into city (city_name) values ('Irbid'),('Ajloun'),('Jarash'),('Amman');