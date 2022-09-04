from cvzone.FaceDetectionModule import FaceDetector
import cv2
import socket

cap = cv2.VideoCapture(0)

cap.set(3, 1280)
cap.set(4, 960)

detector = FaceDetector(minDetectionCon=0.8)

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ('127.0.0.1', 5052)

while True:
    success, img = cap.read()
    
    # Finding the faces
    img, bboxs = detector.findFaces(img)
        
    if bboxs:
        center = bboxs[0]['center']
        print(center)
        data = str.encode(str(center))
        sock.sendto(data, serverAddressPort)
    
    cv2.imshow("imaage", img)
    cv2.waitKey(1)
    
    