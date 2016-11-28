function deleteResultHandler(response) {
    if (response.ok) {
        window.location.reload();
    } else {
        alert(response.message);
    }
}