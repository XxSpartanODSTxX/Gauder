[ix,iy]= size(flt1);
in = max(ix,iy)
jn=2;
while jn < in 
    jn = jn*2;
end
bigflt=[[flt1;zeros(jn-ix,iy)],zeros(jn,jn-iy)];


quad = quadTree(bigflt,0,7);
global qMap; 
qMap= zeros(jn,jn);

 global jcount;
 jcount=1;
  global index;
  index=0;
 
qMap = draw(quad, qMap,0,0, jn/2)

global fileID;
global maxSize;
global data;
data= zeros(index,4);
maxSize= jn;
fileID = fopen('collisiondata.h','w');
fprintf(fileID,'/*   Data set for Collision for Darc 1000\n   Author: Klaus-Dieter Rupp */\n');
fprintf(fileID,'#ifndef motionCollisionData_h\n#define motionCollisionData_h\n');
fprintf(fileID,'#define MAX_SIZE %d\n', jn);
fprintf(fileID,'float theta1_min  = %f;\n', min(theta1));
fprintf(fileID,'float theta1_max  = %f;\n', max(theta1));
fprintf(fileID,'float theta1_step = %f;\n', theta1(2)-theta1(1));
fprintf(fileID,'float theta2_min  = %f;\n', min(theta2));
fprintf(fileID,'float theta2_max  = %f;\n', max(theta2));
fprintf(fileID,'float theta2_step = %f;\n', theta2(2)-theta2(1));
fprintf(fileID,'short data[][4]={\n');
fprintf(fileID,'{ 0, 0, 0, 0 }, // unused Index\n');  % Damit Index 0 nicht genutzt wird

write(quad, 0,0, jn/2)
fprintf(fileID,'#endif\n');
fclose(fileID);
 img1= bigflt(1:ix,1:iy)
 img1(img1>1)=1;
 imagesc(img1)
 figure;
 img2= qMap ~= 77;
 imagesc(img2(1:ix,1:iy)+img1);
 qMap= zeros(jn,jn);
 decodeDat(index, 0,0,maxSize/2);
 imagesc(qMap(1:ix,1:iy))
 hold on
 for kkk= 0:500
 wi1= rand(1)*ix; wi2 =rand(1)*iy;
 kolli= checkDecodeDat(wi1,wi2, index,0,0, maxSize/2)
 if kolli == 77
 plot(wi2,wi1,'r*');
 else
 plot(wi2,wi1,'m+');
 end
 drawnow
 end
hold on
 
 function index = write(quad,i,k, ik)
 global fileID;
 global index;
 global data;
     kn= [0 0; ik 0; 0 ik; ik ik];
     q=[0 0 0 0];
     global jcount;
     global maxSize;
     if iscell(quad)
       %  fprintf(fileID,'+');
         ni=0;
         for n=1:4
          if iscell(quad{n})
              q(n) =write(quad{n},i+kn(n,1),k+kn(n,1), ik/2  );
          else
              fprintf('i=%d k=%d ik=%d n=%d j=%d x=%d\n', i, k, ik, n, jcount, quad{n})
              q(n)= -quad{n};
              ni = ni+1;
              jcount=jcount+1;
          end

         end
      
         if ik < maxSize/2 
            fprintf(fileID,'{%d, %d, %d, %d},\n',q(1),q(2),q(3),q(4));
         else
            fprintf(fileID,'{%d, %d, %d, %d}};\n',q(1),q(2),q(3),q(4));
         end
            index = index+1;
            data(index,:)= q;
          
     end
 end
 
function qMap =draw(quad, qMap,i,k, ik)
  
kn= [0 0; ik 0; 0 ik; ik ik];
if iscell(quad)
    for ind= 1:4
    if iscell(quad{ind})
        qMap =draw(quad{ind},qMap,i+kn(ind,1),k+kn(ind,2), ik/2 );
    else
        qMap(i+kn(ind,1)+(1:ik),k+kn(ind,2)+(1:ik))= quad{ind};
    end
    end
end
end

% ***********************************************
function decodeDat(ik,nn,mm, level)
 global data; 
 global qMap; 
for n= 0:1
    for m= 0:1
       nm=n+m+m+1;
       if data(ik,nm)>0 % Index
          decodeDat(data(ik,nm),nn+n*level,mm+m*level,level/2);
       else
         qMap(nn+n*level+(1:level),mm+m*level+(1:level))= -data(ik,nm);
       end
    end
end
end


function ret = checkDecodeDat(wi1,wi2, ik,nn,mm, level)
 global data; 
 global qMap; 
 n=0; m=0;
 if wi1>level+nn
     n = 1;
 end
 if wi2>level+mm
     m = 1;
 end
  nm=n+m+m+1;
       if data(ik,nm)>0 % Index
          ret = checkDecodeDat(wi1,wi2,data(ik,nm),nn+n*level,mm+m*level,level/2);
       else
         ret = -data(ik,nm);
       end
end

% if  isfield(quad,'q2')
%     if isstruct(quad.q2)
%        qMap =draw(quad.q2,qMap,i+ik, k, ik/2 )
%     else
%         qMap((i+ik)+(1:ik),k+(1:ik))= quad.q2
%        
%     end
% end
% if  isfield(quad,'q3')
%     if isstruct(quad.q3)
%        qMap =draw(quad.q3,qMap,i, k+ik, ik/2 )
%     else
%       qMap(i+(1:ik),(k+ik) +(1:ik))= quad.q3
%      end
% end
% if  isfield(quad,'q4')
%     if isstruct(quad.q4)
%        qMap =draw(quad.q4,qMap, i+ik, k+ik, ik/2 )
%     else
%       qMap((i+ik)+(1:ik),(k+ik) +(1:ik))= quad.q4
%        
%     end
% end
