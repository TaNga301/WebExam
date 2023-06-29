var click_eye=document.querySelector(".input-text .fa-eye-slash");
var eye=document.querySelector(".passcode");
var in_pass=document.querySelector("#password_input");
 click_eye.addEventListener('click',function(){
     
    if(in_pass.type=='password'){
        in_pass.type="text";
        eye.classList.remove('fa-eye-slash');
        eye.classList.add('fa-eye');
        in_pass.classList.add('warning');
    }
    else{
        in_pass.type="password";
        eye.classList.add('fa-eye-slash');
        eye.classList.remove('fa-eye');
        in_pass.classList.remove('warning');
    }
 });