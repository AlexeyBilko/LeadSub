let changepswBtn = document.getElementById('change-password')
changepswBtn.onclick = function ()
{
    let changePasswordArea=document.getElementById('change-password-area')
    if (changePasswordArea.className != 'd-block') {

        changePasswordArea.className="d-block"
    }
    else {

        changePasswordArea.className="d-none"

    }
}