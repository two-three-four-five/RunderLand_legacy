using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GPS : MonoBehaviour
{
    public float latitude;
    public float longitude;
	public Text	statusText;
    public Text latitudeText;
    public Text longitudeText;

    void Start()
    {
        // 위치 서비스 초기화
        Input.location.Start(5);

        // 위치 서비스 활성화 확인
        if (Input.location.isEnabledByUser)
        {
            // 위치 서비스 초기화까지 대기
            StartCoroutine(InitializeGPS());
        }
        else
        {
            Debug.Log("GPS not available");
        }
    }

    IEnumerator InitializeGPS()
    {
        // 위치 서비스 초기화 중일 때까지 대기
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1);
        }

        // 위치 서비스 초기화가 성공한 경우
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // GPS 데이터 갱신 시작
            StartCoroutine(UpdateGPSData());
        }
        else
        {
            Debug.Log("Failed to initialize GPS");
        }
    }

    IEnumerator UpdateGPSData()
    {
		int	gps_connect = 0;
        while (true)
        {
            // GPS 데이터 업데이트 대기
            yield return new WaitForSeconds(1);

            // 현재 GPS 데이터 가져오기
            LocationInfo currentGPSPosition = Input.location.lastData;

            // 위도와 경도 텍스트 업데이트
			gps_connect++;
            latitude = currentGPSPosition.latitude;
            longitude = currentGPSPosition.longitude;
            latitudeText.text =  currentGPSPosition.latitude.ToString();
            longitudeText.text = currentGPSPosition.longitude.ToString();
			statusText.text = (Input.location.status == LocationServiceStatus.Running ? "run" : "not run") + gps_connect.ToString();
        }
    }
}
