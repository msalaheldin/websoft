<?php
/**
 * A page controller
 */
require "config.php";
require "src/functions.php";

// Connect to the database
$db = connectDatabase($dsn);

// Prepare and execute the SQL statement
$stmt = $db->prepare("SELECT * FROM tech");
$stmt->execute();

// Get the results as an array with column names as array keys
$res = $stmt->fetchAll();

?>

<?php 
$pageTitle = "Read";
require __DIR__ . "/view/header.php" 
?>
<h1>Read the values in the database</h1>
<?php require __DIR__ . "/view/dbTable.php" ?>

<?php require __DIR__ . "/view/footer.php" ?>
