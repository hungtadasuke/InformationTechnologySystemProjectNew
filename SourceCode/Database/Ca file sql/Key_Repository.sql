create database Key_Repository;

use Key_Repository;

create table key_repo (
	cryptographic_id varchar(3) primary key,
	encrypted_key ntext not null,
	key_of_encrypted_key ntext not null,
);

insert into key_repo values ('K01', N'ấẵỵáọêẹếợèặaễệeủgứũăqóhõuãtùữìịỗkoềỡđầâửộrẩắỹíựĩxởẽổảỉơỷyớlôdẫcạậồvpúmểòýụẳừéẻỳờbỏiằốnàsư', '133'),
							('K02', '605/449/664/584/820/449/820/481/799/605/820/470/777/664/664', '1,3,7,13,26,65,119,267#523#467');

select * from key_repo;