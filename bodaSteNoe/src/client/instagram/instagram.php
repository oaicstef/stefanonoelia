<?php
header('Content-type: application/json');

$user_id = '343055137';
$access_token = '343055137.1677ed0.82fe8f3fe22d409887cb02784daab5d2';
$hashtag = 'noeste2016';

$api_url = 'https://api.instagram.com/v1/tags/' . $hashtag . '/media/recent?access_token=' . $access_token;

function fetchData($url){
	$ch = curl_init();
	curl_setopt($ch, CURLOPT_URL, $url);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
	curl_setopt($ch, CURLOPT_TIMEOUT, 20);
	curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
	curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);
	$result = curl_exec($ch);
	curl_close($ch);
	return $result;
}

$result = fetchData($api_url);
$result = json_encode($result);

echo $result;

?>