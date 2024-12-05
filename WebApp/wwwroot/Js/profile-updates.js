function triggerFileInputClick(element) {
    console.log("triggerFileInputClick called");
    element.click();
}

function handleDragOver(event) {
    event.preventDefault();
    event.stopPropagation();
    event.dataTransfer.dropEffect = 'copy';
    event.currentTarget.classList.add('drag-over');
    console.log("handleDragOver called");
}

function handleDragLeave(event) {
    event.preventDefault();
    event.stopPropagation();
    event.currentTarget.classList.remove('drag-over');
    console.log("handleDragLeave called");
}

function handleDrop(event, dotNetHelper) {
    event.preventDefault();
    event.stopPropagation();
    event.currentTarget.classList.remove('drag-over');
    const files = event.dataTransfer.files;
    if (files.length > 0) {
        console.log("handleDrop called, file dropped: " + files[0].name);
        dotNetHelper.invokeMethodAsync('HandleFileDrop', files[0]);
    }
}

function openModal() {
    console.log("openModal called");
    document.querySelector('.modal').style.display = 'flex';
}

function closeModal() {
    console.log("closeModal called");
    document.querySelector('.modal').style.display = 'none';
}