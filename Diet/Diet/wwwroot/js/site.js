// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const currentDate=document.querySelector(".current-date");
daysTag=document.querySelector(".days"),
prevNextIcon=document.querySelectorAll(".icons span");

let date=new Date(),
currYear=date.getFullYear(),
currMonth=date.getMonth();

const months=["Януари", "Февруари","Март","Април","Май","Юни","Юли",
              "Август","Септември","Октомври","Ноември","Декември"];

const renderCalendar = () => {
	let lastDateMonth=new Date(currYear, currMonth + 1, 0).getDate();
	let liTsg="";

	
	for(let i=1; i<=lastDateMonth; i++){
		liTag+=`<li>${i}</li>`;
	}


	currentDate.innerText=`${months[currMonth]} ${currYear}`;
	daysTag.innerHTML=liTag;
}
renderCalendar();
prevNextIcon.forEach(icon=>{
	icon.addEventsListener("click", () =>{
		currMonth=icon.id === "prev" ? currMonth -1 : currMonth +1;
		renderCalendar();
	});
});