window.initializeDragAndDrop = () => {
    const dropArea = document.querySelector(".drag-drop-area");
    const fileInput = document.getElementById("fileInput");

    if (!dropArea || !fileInput) {
        console.error("Drag-drop area or file input not found.");
        return;
    }

    console.log("Drag-and-drop initialized!");

    // Trigger file input when the drag-drop area is clicked
    dropArea.addEventListener("click", () => {
        fileInput.click(); // Open file dialog
    });

    // Drag-over event handler
    dropArea.addEventListener("dragover", (event) => {
        event.preventDefault(); // Prevent default behavior
        dropArea.style.backgroundColor = "#e9ecef"; // Highlight the drag area
    });

    // Drag-leave event handler
    dropArea.addEventListener("dragleave", () => {
        dropArea.style.backgroundColor = ""; // Reset background color
    });

    // Drop event handler
    dropArea.addEventListener("drop", async (event) => {
        event.preventDefault(); // Prevent default behavior
        dropArea.style.backgroundColor = ""; // Reset background color

        const files = event.dataTransfer.files;
        if (files.length > 0) {
            const file = files[0]; // Handle the first file only
            console.log("File dropped:", file.name);

            // Trigger the file input change event programmatically
            const dataTransfer = new DataTransfer();
            dataTransfer.items.add(file);
            fileInput.files = dataTransfer.files;

            // Notify Blazor of the selected file
            await fileInput.dispatchEvent(new Event("change"));
        }
    });
};
