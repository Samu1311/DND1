window.initializeDragAndDrop = () => {
    console.log("Initializing drag-and-drop...");

    const dropArea = document.querySelector(".drag-drop-area");
    const fileInput = document.getElementById("fileInput");

    if (!dropArea || !fileInput) {
        console.error("Drag-drop area or file input not found.");
        return;
    }

    dropArea.addEventListener("click", () => {
        fileInput.click();
    });

    dropArea.addEventListener("dragover", (event) => {
        event.preventDefault();
        dropArea.style.backgroundColor = "#e9ecef";
    });

    dropArea.addEventListener("dragleave", () => {
        dropArea.style.backgroundColor = "";
    });

    dropArea.addEventListener("drop", (event) => {
        event.preventDefault();
        dropArea.style.backgroundColor = "";

        const files = event.dataTransfer.files;
        if (files.length > 0) {
            const file = files[0];
            console.log("File dropped:", file.name);

            const dataTransfer = new DataTransfer();
            dataTransfer.items.add(file);
            fileInput.files = dataTransfer.files;

            fileInput.dispatchEvent(new Event("change"));
        }
    });
};
