# Assignment 3

## Overview

This project is an assignment focused on building a search interface to find teachers. It includes controllers, API controllers, and views to manage the interaction with the Teacher table from School Database.

## Assignment Components

### Controllers

#### TeacherController
This controller has the below listed methods :-
- **Index():** Calls `Index.cshtml` view.
- **List():** Calls `getAllTeachers()` from `TeachersDataController` and renders `List.cshtml`.
- **Show(id):** Calls `getTeacher()` from `TeachersDataController` and renders `Show.cshtml`.
- **Search(firstName, hireDate, salary):** 
    - Calls `List()` of `TeacherController` OR 
    - Calls `searchTeachers()` from `TeachersDataController` and renders `Search.cshtml`.

### API Controllers

#### TeachersDataController
This API controller has the below listed methods :-
- **getAllTeachers():** Retrieves a list of all teachers.
- **getTeacher(id):** Retrieves details for a specific teacher.
- **searchTeachers(firstName, hireDate, salary):** Searches for teachers based on different combinations of first name, date hired, and salary parameters based on what data the user enters in the Search form

### Views

- **Index.cshtml:** This Page lets the user do two things - 
    - Shows a form to search for teachers based on first name, date hired, and salary. The user can enter any or all of these inputs OR
    - User can click on a button to view all the teachers.
- **Show.cshtml:** Shows details for a single teacher.
- **List.cshtml:** Shows a List of all teachers as links.
- **Search.cshtml:** Shows either a List of teachers as a result of the search query or shows no results. 

## Flow Diagram
[Link to Diagram](https://drive.google.com/file/d/1SkPpPELAdvndlSik3-HzwUX96NMBiMnF/view?usp=sharing)