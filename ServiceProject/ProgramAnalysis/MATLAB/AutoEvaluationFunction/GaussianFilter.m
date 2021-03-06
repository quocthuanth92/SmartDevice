function [ X, Y, A1, B1, B2, Lo, Hi, M, N  ] = GaussianFilter(InputImage)
%GAUSSIANFILTER Summary of this function goes here
%   Detailed explanation goes here
    I = rgb2gray(InputImage); % convert the image to grey 
    A = fft2(double(I)); % compute FFT of the grey image
    A1=fftshift(A); % frequency scaling
    % Gaussian Filter Response Calculation

    [M N]=size(A); % image size
    R=10; % filter size parameter 
    X=0:N-1;
    Y=0:M-1;
    [X Y]=meshgrid(X,Y);
    Cx=0.5*N;
    Cy=0.5*M;
    Lo=exp(-((X-Cx).^2+(Y-Cy).^2)./(2*R).^2);
    Hi=1-Lo; % High pass filter=1-low pass filter

    % Filtered image=ifft(filter response*fft(original image))

    J=A1.*Lo;
    J1=ifftshift(J);
    B1=ifft2(J1);

    K=A1.*Hi;
    K1=ifftshift(K);
    B2=ifft2(K1);

end

