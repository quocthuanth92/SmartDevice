function [ Result, Rate, RunningTime, ErrorStatus] = CheckRealImage( ImagePath, BagPath, MeanMhistRGBPath, RealSamplePath)
%CHECKREALORFAKE Summary of this function goes here
%   Detailed explanation goes here
    ErrorStatus = ' ';

    try
        tic 
         [isReal, ~ , FakeRate] = AnalysisRealOrFake(ImagePath, BagPath, MeanMhistRGBPath );
        I = imread(RealSamplePath);
        I1 = imread(ImagePath);

	[rowsI1 colsI1 numberOfColorChannelsI1] = size(I1);
        I = imresize(I, [rowsI1 colsI1]);

        ref = imresize(I, 0.2);
        A = imresize(I1, 0.2);
        ssimval = ssim(A,ref);
       
        [ X, Y, A1, B1, B2, Lo, Hi, M, N  ] = GaussianFilter(I1);
        R1 = mat2gray(abs(A1));
        V = sum(R1(:));
        RunningTime  = toc;

        if(V > 30)
            if(V < 40 && ( ssimval < 0 && FakeRate > 0) )
                isReal =  0;
                RealRate = 0;
            else
                isReal =  1;
                RealRate = 100;
            end
        else
            isReal =  0;
            RealRate = 0;
        end
        
        Result = isReal;
        Rate = RealRate;
%         OutParm1 = FakeRate;
%         OutParm2 = ssimval;
%         OutParm3 = V;
    
    catch ME
        Result = 0;
        Rate = 0;
        RunningTime = 0;
        ErrorStatus = ['Error in check Real or Fake:' ME.message ' for ' ImagePath];
        
%         OutParm1 = 0; 
%         OutParm2 = 0; 
%         OutParm3 = 0;
    end
end

