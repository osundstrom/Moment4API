# Moment 4 DT191G 

## Info
Uppgiften använder sig av SQLite som databas

I databasen finns det en Tabell som ser ut enligt nedan.
 
### Songs

| id   | titel    | artist    | length   | category  | 
| ---- | -------------- | ---------- | ---------- | -------- |
| 1  | Back in Black  | AC/DC   | 230    | Rock |



## Användning
 Hur man användet API:

| Metod   | Url ändelse    | Beskrivning   | 
| ---- | -------------- | ---------- | 
| GET   | /songs   | Hämtar alla låtar  | 
| POST   | /songs    | Skapar en ny låt   | 
| PUT   | /songs:id    | Ändrar  baserat på id| 
| DELETE   | /songs:id    | Raderar baserat på id | 



