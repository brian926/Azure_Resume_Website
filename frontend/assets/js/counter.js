window.addEventListener('DOMContentLoaded', (event) => {
	getVisitCount();
});

// Get a count of visitors to the site
//const localApi = 'http://localhost:7071/api/GetResumeCounter';
const functionApi = 'https://resumevisitorcounter.azurewebsites.net/api/GetResumeCounter?code=MZhhMvfcBL3AGuB2EHUBXd5hgQmxVF3LxCcgzwHIkiyEl590UlAM1g=='; 
					
const getVisitCount = () => {
	let count = 30;
	fetch(functionApi).then(response => {
			return response.json()
		}).then(response => {
			console.log("Website called function API.");
			count = response.count;
			document.getElementById('counter').innerText = count;
		}).catch(function(error) {
			console.log(error);
		});
return count;
}