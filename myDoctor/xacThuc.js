window.onload = function () {
	render();
};

function render()
{
	recaptchaVerifier.render().then(function (widgetId) {
		window.recaptchaWidgetId = widgetId;
	});
}

function phoneAuth()
{
	debugger
	var a = document.getElementById('number').value;
	var b ="+84";
	var number = b+a;

	firebase.auth().signInWithPhoneNumber(number, this.window.recaptchaVerifier)
    .then((confirmationResult) => {
     
     this. window.confirmationResult = confirmationResult;
      console.log(coderesult);
      alert("Gửi tin nhắn");
    }).catch((error) => {
       alert(error.message);
    });
}

function codeverify(){
	debugger
	var code = document.getElementById('verificationCode').value;
	coderesult.confirm(code).then (function(result){
		alert("Message Verified");
		var user = result.user;
		console.log(user);
		window.location.href="/home/index";
		}).catch(function (error){
		alertI(error.message);	
});
	
}